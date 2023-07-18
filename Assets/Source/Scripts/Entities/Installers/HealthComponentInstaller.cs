using Zenject;

namespace Source.Scripts.Entities.Installers
{
    public class HealthComponentInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            HealthComponent hpComponent = gameObject.GetComponent<HealthComponent>();

            Container.Bind<HealthComponent>().FromInstance(hpComponent).AsSingle();
        }
    }
}