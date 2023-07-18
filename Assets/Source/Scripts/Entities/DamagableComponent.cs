using System;
using Source.Scripts.Entities.Enemies;
using UnityEngine;

namespace Source.Scripts.Entities
{
    public class DamagableComponent : MonoBehaviour
    {
        [field:SerializeField] public float Damage { get; private set; }

        public void Start()
        {
            if (gameObject.TryGetComponent(out DangerBehaviorComponent dangerBehaviorComponent))
            {
                Damage = dangerBehaviorComponent.EnemyItem.defaultDamage;
            }
        }
    }
}