using System.Collections;
using Source.Scripts.Core.Interfaces;
using Source.Scripts.Entities.Enemies.MovementComponent.Interfaces;
using UnityEngine;

namespace Source.Scripts.Entities.Enemies.Animations
{
    public class AnimatorBlendMoveComponent : BaseComponent
    {
        private Animator animator;
        private IMovementSource movementSource;

        [SerializeField] private float MaxValue;
        [SerializeField] private float transitionDuration;
        [SerializeField] private string parameterName;
        private Coroutine transition;

        private void Awake()
        {
            InitComponentInGameObject(out movementSource);
            InitComponentInGameObject(out animator);
        }

        private void OnEnable()
        {
            movementSource.StateMovement += OnHandleMove;
        }

        private void OnDisable()
        {
            movementSource.StateMovement -= OnHandleMove;
        }

        private void OnHandleMove(bool state)
        {
            if (state)
            {
                TransitionValue(MaxValue);
            }
            else
            {
                TransitionValue(0);
            }
        }

        private void TransitionValue(float targetValue)
        {
            if (transition != null)
            {
                StopCoroutine(transition);
                transition = null;
            }
            transition = StartCoroutine(LerpValueCoroutine(targetValue));
        }

        private IEnumerator LerpValueCoroutine(float targetValue)
        {
            float elapsedTime = 0;
            float startValue = animator.GetFloat(parameterName);
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / transitionDuration);
                float newValue = Mathf.Lerp(startValue, targetValue, normalizedTime);
                animator.SetFloat(parameterName, newValue);
                yield return null;
            }
            transition = null;
        }
    }
}