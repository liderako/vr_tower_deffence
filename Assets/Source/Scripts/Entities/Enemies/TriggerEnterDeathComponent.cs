using Source.Scripts.Entities.Enemies.Interfaces;
using Source.Scripts.ZXRCore.Avatar;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Enemies
{
    public class TriggerEnterDeathComponent : MonoBehaviour, IArrowHittable
    {
        [Inject] private IDeathComponent deathComponent;
        private const string TagWeapon = "Weapon";
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(TagWeapon))
            {
                deathComponent.CallDeath(other.contacts[0].point);
            }
            else if (other.gameObject.TryGetComponent(out DangerColliderComponent dangerColliderComponent))
            {
                if (dangerColliderComponent.State)
                {
                    deathComponent.CallDeath(other.contacts[0].point);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out AvatarHandComponent avatarHandComponent))
            {
                if (avatarHandComponent.IsFree && avatarHandComponent.dangerColliderComponent.State)
                {
                    deathComponent.CallDeath(other.transform.position);
                }
            }
        }
        
        public void Hit(Arrow arrow)
        {
            deathComponent.CallDeath(arrow.transform.position);
        }
    }
}