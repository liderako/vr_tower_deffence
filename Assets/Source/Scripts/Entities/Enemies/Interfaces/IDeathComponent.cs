using UnityEngine;
using System;

namespace Source.Scripts.Entities.Enemies.Interfaces
{
    public interface IDeathComponent
    {
        // vector for Hit position
        public event Action<Vector3> DeathPhysicsAction;
        
        public event Action DeathAction; 

        public void CallDeath(Vector3 death);
    }
}