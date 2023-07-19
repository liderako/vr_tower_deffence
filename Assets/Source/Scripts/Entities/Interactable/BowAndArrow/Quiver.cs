using Game.Installers.Factory;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Zenject;
using ZXRCore.Interactable;

namespace Source.Core.Interactable.BowAndArrow
{
    public class Quiver : XRBaseInteractable
    {
        [SerializeField] private GameObject objectPrefab;
        [Inject] private GameObjectFactory gameObjectFactory;
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            CreateAndSelect(args);
        }

        private void CreateAndSelect(SelectEnterEventArgs args)
        {
            GameObject gameObject = gameObjectFactory.Create(objectPrefab, args.interactorObject.transform.position);
            gameObject.transform.rotation = args.interactorObject.transform.rotation;
            if (gameObject.TryGetComponent(out ZXRGrabInteractableTwoAttach zxrGrabInteractableTwoAttach))
            {
                interactionManager.SelectEnter(args.interactorObject, zxrGrabInteractableTwoAttach);   
            }
            else if (gameObject.TryGetComponent(out ZXRGrabInteractable zxrGrabInteractable))
            {
                interactionManager.SelectEnter(args.interactorObject, zxrGrabInteractable);   
            }
        }
    }
}