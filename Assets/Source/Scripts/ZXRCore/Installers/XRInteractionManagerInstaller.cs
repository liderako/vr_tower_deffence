using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Zenject;

namespace Source.Scripts.Core.Installers
{
    public class XRInteractionManagerInstaller : MonoInstaller
    {
        [SerializeField] private XRInteractionManager xrInteractionManager;
        
        public override void InstallBindings()
        {
            Container.Bind<XRInteractionManager>().FromInstance(xrInteractionManager).AsSingle();
        }
    }
}