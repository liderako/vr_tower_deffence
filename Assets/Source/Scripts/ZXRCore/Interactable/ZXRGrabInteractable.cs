using UnityEngine.XR.Interaction.Toolkit;
using Zenject;

namespace ZXRCore.Interactable
{
    public class ZXRGrabInteractable : XRGrabInteractable
    { 
        [Inject]
        private void InitializeInteractionManager(XRInteractionManager xrInteractionManager)
        {
            interactionManager = xrInteractionManager;
        }
    }
}