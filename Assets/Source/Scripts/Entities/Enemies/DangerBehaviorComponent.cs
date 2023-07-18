using System;
using Source.Configs;
using Source.Entities.Interfaces;
using Source.Scripts.Core.Interfaces;
using Source.Scripts.Entities.Enemies.Interfaces;
using Source.Scripts.Entities.Enemies.MovementComponent;
using UniRx;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Enemies
{
    // it's a simple demo class without state machine patterns
    public class DangerBehaviorComponent : BaseComponent, IDeathListener
    {
        [Inject] public EnemyItem EnemyItem { get; private set; }
        public event Action StartAttack;
        private HealthComponent targetHeathComponent;
        private DamagableComponent damagableComponent;
        private float lastTimeAttack;
        public bool AttackInProgress;
        [Inject] private IDeathComponent deathComponent;

        private enum StateBehavior
        {
            idle,
            move,
            fight
        }
        
        private BaseEnemyMovementComponent baseEnemyMovementComponent;

        private StateBehavior currentState;
        
        private void Awake()
        {
            InitComponentInGameObject(out baseEnemyMovementComponent);
            InitComponentInGameObject(out damagableComponent);
        }

        private void Start()
        {
            targetHeathComponent = baseEnemyMovementComponent.target.GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            baseEnemyMovementComponent.StateMovement += OnHandleStateMovement;
            deathComponent.DeathAction += OnHandleDeath;
            Observable.EveryUpdate()
                .Where(_ => currentState == StateBehavior.fight)
                .Subscribe(_ => UpdateFightState())
                .AddTo(this);
        }
        
        private void OnDisable()
        {
            baseEnemyMovementComponent.StateMovement -= OnHandleStateMovement;
            deathComponent.DeathAction -= OnHandleDeath;
        }

        public void OnHandleDeath()
        {
            enabled = false;
        }

        private void OnHandleStateMovement(bool state)
        {
            if (!state)
            {
                if (baseEnemyMovementComponent.IsClose)
                {
                    ChangeState(StateBehavior.fight);
                }
            }
            else
            {
                lastTimeAttack = Time.time;
                ChangeState(StateBehavior.move);
            }
        }
        
        private void ChangeState(StateBehavior newStateBehavior)
        {
            currentState = newStateBehavior;
        }

        private void UpdateFightState()
        {
            if (!AttackInProgress && Time.time - lastTimeAttack > EnemyItem.coolDownAttack)
            {
                lastTimeAttack = Time.time;
                StartAttack?.Invoke();
            }
        }

        public void ChangeStateAttack(bool state)
        {
            AttackInProgress = state;
        }

        // call in animation
        public void Attack()
        {
            targetHeathComponent.Hit(damagableComponent);
            lastTimeAttack = Time.time;
        }
    }
}