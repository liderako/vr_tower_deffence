using Source.Configs;
using Source.Entities.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities
{
    public class HealthComponent : MonoBehaviour
    {
        [Inject] private AliveItem aliveItem;
        [Inject] private IDeathComponent deathComponent;

        private float currentHp;
        public bool Dead { get; private set; }

        private void Awake()
        {
            currentHp = aliveItem.hp;
        }

        public void Hit(DamagableComponent damagableComponent)
        {
            if (!Dead)
            {
                currentHp -= damagableComponent.Damage;
                if (currentHp <= 0)
                {
                    currentHp = 0;
                    Dead = true;
                    deathComponent.CallDeath(damagableComponent.transform.position);
                }
            }
        }
    }
}