using Source.Entities.Interfaces;
using Source.Scripts.Core.Interfaces;
using Zenject;

namespace Source.Scripts.Entities.Enemies
{
    public class HittableColliderComponent : BaseComponent, IHittable
    {
        [Inject] private HealthComponent healthComponent;

        public void Hit(DamagableComponent component)
        {
            if (!healthComponent.Dead)
            {
                healthComponent.Hit(component);
            }
            else if (healthComponent.TryGetComponent(out RagdollComponent ragdollComponent))
            {
                ragdollComponent.ImpactOfHit(component.transform.position);
            }
        }
    }
}