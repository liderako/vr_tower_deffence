using UnityEngine;
using Zenject;

namespace Source.Scripts.Systems.SlowMotion
{
    [RequireComponent(typeof(VelocityEstimator))]
    public class SlowMotionChildComponent : MonoBehaviour
    {
        [Inject] private SlowMotionSystem slowMotionSystem;

        private void Awake()
        {
            slowMotionSystem.AddChild(GetComponent<VelocityEstimator>());
        }
    }
}