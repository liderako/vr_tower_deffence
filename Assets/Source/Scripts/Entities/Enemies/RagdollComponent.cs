using System.Collections.Generic;
using Source.Entities.Interfaces;
using Source.Scripts.Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Enemies
{
    public class RagdollComponent : BaseComponent
    {
        [Inject] private IDeathComponent deathComponent;
        [SerializeField] private float rangeOverlapExplo = 0.3f;
        private const string RagDollTag = "RagdollPart";
        private List<Rigidbody> rigidbodies;

        private void Awake()
        {
            InitRigidbodies();
        }

        private void OnEnable()
        {
            deathComponent.DeathPhysicsAction += ImpactOfHit;
        }

        private void OnDisable()
        {
            deathComponent.DeathPhysicsAction -= ImpactOfHit;
        }

        private void InitRigidbodies()
        {
            Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
            rigidbodies = new List<Rigidbody>();
            for (int i = 0; i < rbs.Length; i++)
            {
                if (rbs[i].CompareTag(RagDollTag))
                {
                    rigidbodies.Add(rbs[i]);
                    rbs[i].isKinematic = true;
                }
            }
        }

        public void ImpactOfHit(Vector3 hitPosition)
        {
            for (int i = 0; i < rigidbodies.Count; i++)
            {
                rigidbodies[i].isKinematic = false;
            }
            
            Collider[] colliders = Physics.OverlapSphere(hitPosition, rangeOverlapExplo);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(1000, hitPosition, rangeOverlapExplo);
                }
            }
        }
    }
}