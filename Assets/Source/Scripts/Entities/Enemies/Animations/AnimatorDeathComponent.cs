using Source.Scripts.Core.Interfaces;
using Source.Scripts.Entities.Enemies.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Entities.Enemies.Animations
{
    public class AnimatorDeathComponent : BaseComponent, IDeathListener
    {
        [Inject] private IDeathComponent deathComponent;
        private Animator animator;

        private void Awake()
        {
            InitComponentInGameObject(out animator);
        }

        private void OnEnable()
        {
            deathComponent.DeathAction += OnHandleDeath;
        }

        private void OnDisable()
        {
            deathComponent.DeathAction -= OnHandleDeath;
        }

        public void OnHandleDeath()
        {
            animator.enabled = false;
        }
    }
}