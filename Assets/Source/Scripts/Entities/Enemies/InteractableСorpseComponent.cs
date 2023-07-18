using Source.Entities.Interfaces;
using UnityEngine;
using Zenject;
using ZXRCore.Interactable;

namespace Source.Scripts.Entities.Enemies
{
    public class InteractableСorpseComponent : MonoBehaviour
    {
        [Inject] private IDeathComponent deathComponent;
        private const int InteractableLayer = 7;

        private void OnEnable()
        {
            deathComponent.DeathAction += OnHandleDeath;
        }

        private void OnDisable()
        {
            deathComponent.DeathAction -= OnHandleDeath;
        }

        private void OnHandleDeath()
        {
            ZXRGrabInteractable[] grabInteractables = GetComponentsInChildren<ZXRGrabInteractable>();
            for (int i = 0; i < grabInteractables.Length; i++)
            {
                if (!grabInteractables[i].enabled && grabInteractables[i].TryGetComponent(out Rigidbody rigidbody))
                {
                    grabInteractables[i].enabled = true;
                    grabInteractables[i].gameObject.layer = InteractableLayer;
                }
            }
        }
    }
}