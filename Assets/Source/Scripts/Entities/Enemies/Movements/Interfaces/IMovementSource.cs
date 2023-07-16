using System;

namespace Source.Scripts.Entities.Enemies.MovementComponent.Interfaces
{
    public interface IMovementSource
    {
        public event Action<bool> StateMovement;
    }
}