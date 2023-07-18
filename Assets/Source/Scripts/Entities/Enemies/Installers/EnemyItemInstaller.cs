using Source.Configs;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Enemies.Installers
{
    public class EnemyItemInstaller : MonoInstaller
    {
        [SerializeField] private EnemyItem item;

        public override void InstallBindings()
        {
            Container.Bind<EnemyItem>().FromInstance(item).AsSingle();
        }
    }
}