using Source.Scripts.Entities;

namespace Source.Entities.Interfaces
{
    public interface IHittable
    {
        void Hit(DamagableComponent damagableComponent);
    }
}