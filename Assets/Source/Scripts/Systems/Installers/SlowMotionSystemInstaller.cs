using Source.Scripts.Systems.SlowMotion;
using Zenject;

namespace Source.Scripts.Systems.Installers
{

    public class SlowMotionSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SlowMotionSystem>().FromInstance(new SlowMotionSystem()).AsSingle();
        }
    }
}