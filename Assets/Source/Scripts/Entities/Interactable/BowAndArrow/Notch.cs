﻿using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ZXRCore.Interactable;

namespace Source.Core.Interactable.BowAndArrow
{
    public class Notch : XRSocketInteractor
    {
        [SerializeField, Range(0, 1)] private float releaseThreshold = 0.05f;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ZXRGrabInteractable Bow;
        public PullMeasurer PullMeasurer { get; private set; }

        public bool CanRelease => PullMeasurer.PullAmount > releaseThreshold;

        protected override void Awake()
        {
            base.Awake();
            PullMeasurer = GetComponentInChildren<PullMeasurer>();
        }
        
        // events for select exited PullMeasurer
        public void ReleaseArrow(SelectExitEventArgs args)
        {
            if (hasSelection)
            {
                audioSource.Play();
                interactionManager.SelectExit(this, firstInteractableSelected);
            }
        }

        public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractor(updatePhase);
            if (Bow.isSelected)
            {
                UpdateAttach();   
            }
        }

        public void UpdateAttach()
        {
            // Move attach when bow is pulled, this updates the renderer as well
            attachTransform.position = PullMeasurer.PullPosition;
        }

        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            // We check for the hover here too, since it factors in the recycle time of the socket
            // We also check that notch is ready, which is set once the bow is picked up
            return QuickSelect(interactable) && CanHover(interactable) && interactable is Arrow && Bow.isSelected;
        }

        private bool QuickSelect(IXRSelectInteractable interactable)
        {
            // This lets the Notch automatically grab the arrow
            return !hasSelection || IsSelecting(interactable);
        }

        private bool CanHover(IXRSelectInteractable interactable)
        {
            if (interactable is IXRHoverInteractable hoverInteractable)
            {
                return CanHover(hoverInteractable);   
            }
            return false;
        }
    }
}
