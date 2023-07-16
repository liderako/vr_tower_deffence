using System;
using Source.Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ZXRCore.Interactable
{
    [System.Serializable]
    public class Haptic
    {
        [Range(0, 1)] [SerializeField] private float intensity;
        [SerializeField] private float duration;   
        
        public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
        {
            if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
            {
                TriggerHaptic(controllerInteractor.xrController);
            }
        }

        public void TriggerHaptic(XRBaseController controller)
        {
            controller.SendHapticImpulse(intensity, duration);
        }

        public bool IsAvailable()
        {
            return intensity > 0;
        }
    }
    
    public class HapticInteractable : BaseComponent
    {
        [SerializeField] private Haptic hapticOnActivated;
        [SerializeField] private Haptic hapticHoverEntered;
        [SerializeField] private Haptic hapticHoverExited;
        [SerializeField] private Haptic hapticSelectEntered;
        [SerializeField] private Haptic hapticSelectExited;

        private XRBaseInteractable interactable;

        private void Awake()
        {
            InitComponentInGameObject(out interactable);
        }

        private void Start()
        {
            if (hapticOnActivated.IsAvailable())
            {
                interactable.activated.AddListener(hapticOnActivated.TriggerHaptic);
            }
            if (hapticHoverEntered.IsAvailable())
            {
                interactable.hoverEntered.AddListener(hapticHoverEntered.TriggerHaptic);
            }
            if (hapticHoverExited.IsAvailable())
            {
                interactable.hoverExited.AddListener(hapticHoverExited.TriggerHaptic);
            }
            if (hapticSelectEntered.IsAvailable())
            {
                interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
            }
            if (hapticSelectExited.IsAvailable())
            {
                interactable.selectExited.AddListener(hapticSelectExited.TriggerHaptic);
            }
        }

        private void OnDestroy()
        {
            interactable.activated.RemoveAllListeners();
            interactable.hoverEntered.RemoveAllListeners();
            interactable.hoverExited.RemoveAllListeners();
            interactable.selectEntered.RemoveAllListeners();
            interactable.selectExited.RemoveAllListeners();
        }
    }
}