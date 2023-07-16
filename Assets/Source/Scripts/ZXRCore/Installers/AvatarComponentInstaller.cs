using Source.Scripts.ZXRCore.Avatar;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core.Installers
{
    public class AvatarComponentInstaller : MonoInstaller
    {
        [SerializeField] private GameObject context;
        
        public override void InstallBindings()
        {
            if (context == null)
            {
                context = gameObject;
            }
            Container.Bind<AvatarComponent>()
                .FromComponentInHierarchy(context)
                .AsSingle();
        }
    }
}