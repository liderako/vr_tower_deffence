using UnityEngine.XR.Interaction.Toolkit;

namespace Source.Scripts.Core.Interactable
{
    public interface IInteractableActivateListener
    {
        public void Interact(ActivateEventArgs activateEventArgs);
    }
}