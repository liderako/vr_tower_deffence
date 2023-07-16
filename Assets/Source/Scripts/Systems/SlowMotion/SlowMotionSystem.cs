
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Source.Scripts.Systems.SlowMotion
{
    public class SlowMotionSystem
    {
        public List<VelocityEstimator> childs;

        public void AddChild(VelocityEstimator child)
        {
            childs.Add(child);
        }

        public SlowMotionSystem()
        {
            childs = new List<VelocityEstimator>(); ;
        }

        public bool IsGameplayState()
        {
            return true;
        }
    }
}