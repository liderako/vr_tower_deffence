using System.Collections;
using Source.Entities.Interfaces;
using Source.Scripts.Entities;
using Source.Scripts.ZXRCore.Avatar;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ZXRCore.Interactable;

namespace Source.Core.Interactable.BowAndArrow
{
    public class Arrow : ZXRGrabInteractable
    {
        [SerializeField] private float speed = 2000.0f;

        private new Rigidbody rigidbody;
        private DangerColliderComponent dangerColliderComponent;
        private ArrowCaster caster;

        private bool launched = false;
        private RaycastHit hit;

        protected override void Awake()
        {
            base.Awake();
            rigidbody = GetComponent<Rigidbody>();
            caster = GetComponent<ArrowCaster>();
            dangerColliderComponent = GetComponent<DangerColliderComponent>();
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            Debug.Log("args.interactorObject is Notch notch " + args.interactorObject is Notch);
            if (args.interactorObject is Notch notch)
            {
                Debug.Log("args.324444444 is Notch notch ");
                if (notch.CanRelease)
                {
                    LaunchArrow(notch);   
                }
            }
        }

        private void LaunchArrow(Notch notch)
        {
            Debug.Log(launched);
            launched = true;
            ApplyForce(notch.PullMeasurer);
            dangerColliderComponent.State = true;
            StartCoroutine(LaunchRoutine());
        }

        private void ApplyForce(PullMeasurer pullMeasurer)
        {
            Debug.Log(transform.forward * (pullMeasurer.PullAmount * speed));
            rigidbody.AddForce(transform.forward * (pullMeasurer.PullAmount * speed));
        }

        private IEnumerator LaunchRoutine()
        {
            // Set direction while flying
            while (!caster.CheckForCollision(out hit))
            {
                SetDirection();
                yield return null;
            }

            Debug.Log("Collide");
            dangerColliderComponent.State = false;
            // Once the arrow has stopped flying
            DisablePhysics();
            ChildArrow(hit);
            CheckForHittable(hit);
        }

        private void SetDirection()
        {
            if (rigidbody.velocity.z > 0.5f)
            {
                transform.forward = rigidbody.velocity;   
            }
        }

        private void DisablePhysics()
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            GetComponent<Collider>().enabled = false;
        }

        private void ChildArrow(RaycastHit hit)
        {
            transform.SetParent(hit.transform);
        }

        public virtual void CheckForHittable(RaycastHit hit)
        {
            if (hit.transform.TryGetComponent(out IHittable hittable))
            {
                hittable.Hit(GetComponent<DamagableComponent>());   
            }
        }
        
        private void Update()
        {
            if (launched)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, rigidbody.velocity, Time.deltaTime * speed, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }

        public override bool IsSelectableBy(IXRSelectInteractor interactor)
        {
            return base.IsSelectableBy(interactor) && !launched;
        }
    }
}