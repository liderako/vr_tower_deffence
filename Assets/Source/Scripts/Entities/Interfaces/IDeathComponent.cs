using System;
using UnityEngine;

namespace Source.Entities.Interfaces
{
    public interface IDeathComponent
    {
        // vector for Hit position
        public event Action<Vector3> DeathPhysicsAction;
        
        public event Action DeathAction; 

        public void CallDeath(Vector3 death);
    }
}