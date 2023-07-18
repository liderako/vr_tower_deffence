using System;
using Source.Entities.Interfaces;
using Source.Scripts.Core.Interfaces;
using UnityEngine;

namespace Source.Scripts.Entities.Enemies
{
    public class DeathComponent : BaseComponent, IDeathComponent
    {
        public event Action<Vector3> DeathPhysicsAction;
        public event Action DeathAction;

        public void CallDeath(Vector3 hitPosition)
        {
            DeathPhysicsAction?.Invoke(hitPosition);
            DeathAction?.Invoke();
        }
    }
}