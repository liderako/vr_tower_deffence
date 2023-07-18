using System;
using Source.Scripts.Core.Interfaces;
using UnityEngine;

namespace Source.Scripts.Entities.Enemies.Animations
{
    public class AnimatorAttackListener : BaseComponent
    {
        private Animator animator;
        private DangerBehaviorComponent dangerBehaviorComponent;
        private const string animationAttackName = "Attack";
        
        private void Awake()
        {
            InitComponentInGameObject(out animator);
        }

        private void OnEnable()
        {
            dangerBehaviorComponent.StartAttack += OnHandleStartAttack;
        }

        private void OnDisable()
        {
            dangerBehaviorComponent.StartAttack -= OnHandleStartAttack;
        }

        private void OnHandleStartAttack()
        {
            animator.SetBool(animationAttackName, true);
            dangerBehaviorComponent.ChangeStateAttack(true);
        }

        // call in animation
        private void FinishAttack()
        {
            dangerBehaviorComponent.ChangeStateAttack(false);
        }
    }
}