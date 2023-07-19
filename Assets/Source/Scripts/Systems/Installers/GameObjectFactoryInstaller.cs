using Game.Installers.Factory;
using Zenject;

namespace Game.Installers
{
    public class GameObjectFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameObjectFactory();
        }

        private void BindGameObjectFactory()
        {
            Container.Bind<GameObjectFactory>().To<GameObjectFactory>().AsSingle();
        }
    }
}