using Source.Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using ZXRCore.Interactable;
using UnityEngine.XR.Interaction.Toolkit;

namespace Source.Scripts.ZXRCore.Avatar
{
    public class AvatarHandComponent : BaseComponent
    {
        [SerializeField] private InputActionProperty selectAction;
        private ZXRDirectInteractor zxrDirectInteractor;
        public DangerColliderComponent dangerColliderComponent;
        public bool IsFree = true;

        private void Awake()
        {
            InitComponentInGameObject(out zxrDirectInteractor);
            InitComponentInGameObject(out dangerColliderComponent);
        }

        private void OnEnable()
        {
            selectAction.action.performed += HandleSelect;
            selectAction.action.canceled += HandleCanceled;
            if (zxrDirectInteractor != null)
            {
                zxrDirectInteractor.selectEntered.AddListener(Grab);
                zxrDirectInteractor.selectExited.AddListener(Drop);
            }
        }

        private void OnDisable()
        {
            selectAction.action.performed -= HandleSelect;
            selectAction.action.canceled -= HandleCanceled;
            if (zxrDirectInteractor != null)
            {
                zxrDirectInteractor.selectEntered.RemoveListener(Grab);
                zxrDirectInteractor.selectExited.RemoveListener(Drop);
            }
        }

        private void Grab(SelectEnterEventArgs args)
        {
            IsFree = false;
            dangerColliderComponent.State = false;
        }

        private void Drop(SelectExitEventArgs args)
        {
            IsFree = true;
            dangerColliderComponent.State = false;
        }
        
        private void HandleCanceled(InputAction.CallbackContext callbackContext)
        {
            dangerColliderComponent.State = false;
        }

        private void HandleSelect(InputAction.CallbackContext callbackContext)
        {
            if (IsFree)
            {
                dangerColliderComponent.State = true;
            }
        }
    }
}