using System;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Systems.SlowMotion
{
    public class SlowMotionComponent : MonoBehaviour
    {
        [Inject] private SlowMotionSystem slowMotionSystem;
        [SerializeField] private float minTimeScale = 0.05f;
        [SerializeField] private float sensitivity = 0.8f;

        private float initialFixedDeltaTime;

        private void Awake()
        {
            initialFixedDeltaTime = Time.fixedDeltaTime;
        }

        private void Update()
        {
            float t = 0;
            for (int i = 0; i < slowMotionSystem.childs.Count; i++)
            {
                t += slowMotionSystem.childs[i].GetVelocityEstimate().magnitude;
            }
            Time.timeScale = Mathf.Clamp01(minTimeScale + t * sensitivity);
            Time.fixedDeltaTime = initialFixedDeltaTime * Time.timeScale;
        }
    }
}