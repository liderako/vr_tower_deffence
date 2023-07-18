using Source.Configs;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Installers
{
    public class AliveItemInstaller : MonoInstaller
    {
        [SerializeField] private AliveItem AliveItem;

        public override void InstallBindings()
        {
            Container.Bind<AliveItem>().FromInstance(AliveItem).AsSingle();
        }
    }
}