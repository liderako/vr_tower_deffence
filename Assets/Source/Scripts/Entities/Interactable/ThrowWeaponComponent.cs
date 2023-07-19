using Source.Entities.Interfaces;
using Source.Scripts.Core.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ZXRCore.Interactable;

namespace Source.Scripts.Entities.Interactable
{
    public class ThrowWeaponComponent : BaseComponent
    {
        [SerializeField] private float speed;
        private Rigidbody rigidbody;
        [SerializeField] private Collider collider;
        private bool launched;
        private ZXRGrabInteractable zxrGrabInteractable;

        private void Awake()
        {
            InitComponentInGameObject(out rigidbody);
            InitComponentInGameObject(out zxrGrabInteractable);
        }

        private void OnEnable()
        {
            zxrGrabInteractable.selectEntered.AddListener(PickUp);
            zxrGrabInteractable.selectExited.AddListener(Throw);
            Observable.EveryUpdate()
                .Where(_ => launched)
                .Subscribe(_ => UpdateRotate())
                .AddTo(this);
        }

        private void Throw(SelectExitEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
            {
                rigidbody.isKinematic = false;
                rigidbody.useGravity = true;
                launched = true;
            }
        }

        private void PickUp(SelectEnterEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
            {
                launched = false;
            }
        }

        private void OnDisable()
        {
            zxrGrabInteractable.selectEntered.RemoveListener(PickUp);
            zxrGrabInteractable.selectExited.RemoveListener(Throw);
        }

        private void UpdateRotate()
        {
            if (launched)
            {
                Quaternion currentRotation = transform.rotation;
                Quaternion zRotation = Quaternion.Euler(0f, 0f, speed * Time.fixedDeltaTime);
                Quaternion newRotation = currentRotation * zRotation;
                transform.rotation = newRotation;
            }
        }

        public void OnCollisionEnter(Collision other)
        {
            if (launched)
            {
                launched = false;
                rigidbody.isKinematic = true;
                rigidbody.useGravity = false;
                transform.SetParent(other.transform);
                if (other.transform.TryGetComponent(out IHittable hittable))
                {
                    hittable.Hit(GetComponent<DamagableComponent>());   
                }
            }
        }
    }
}