using Source.Scripts.Entities.Enemies.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Enemies.Installers
{
    public class DeathComponentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            IDeathComponent deathComponent = gameObject.GetComponent<IDeathComponent>();

            Container.BindInterfacesAndSelfTo<IDeathComponent>().FromInstance(deathComponent).AsSingle();
        }
    }
}