using System;
using Source.Entities.Interfaces;
using Source.Scripts.Core.Interfaces;
using Source.Scripts.Entities.Enemies.Interfaces;
using Source.Scripts.Entities.Enemies.MovementComponent.Interfaces;
using Source.Scripts.ZXRCore.Avatar;
using UniRx;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Enemies.MovementComponent
{
    public abstract class BaseEnemyMovementComponent : BaseComponent, IDeathListener, IMovementSource
    {
        [field:SerializeField] public float MinDistance { get; set; }
        [Inject] protected IDeathComponent deathComponent;
        public virtual event Action<bool> StateMovement;
        public Transform target { get; set; }
        public bool IsClose { get; set; }
        public bool IsMove { get; set; }

        protected virtual void OnEnable()
        {
            deathComponent.DeathAction += OnHandleDeath;
            Activate();
        }

        protected virtual void OnDisable()
        {
            deathComponent.DeathAction -= OnHandleDeath;
        }
        
        public virtual void Activate()
        {
            Observable.EveryUpdate()
                .Where(_ => !IsClose)
                .Subscribe(_ => Move())
                .AddTo(this);
            Observable.EveryLateUpdate()
                .Where(_ => !IsClose)
                .Subscribe(_ => CalculateDistance())
                .AddTo(this);
        }

        public virtual void CalculateDistance()
        {
            if (Vector3.Distance(target.position, transform.position) < MinDistance)
            {
                IsClose = true;
                ChangeStateMovement(false);
            }
            else
            {
                IsClose = false;
            }
        }

        protected abstract void Move();

        public void ChangeStateMovement(bool state)
        {
            if (IsMove != state)
            {
                IsMove = state;
                StateMovement?.Invoke(IsMove);
            }
        }
        
        public virtual void OnHandleDeath()
        {
            enabled = false;
        }
    }
}