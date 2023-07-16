using System;
using Source.Scripts.Core.Interfaces;
using UnityEngine;

namespace Source.Scripts.ZXRCore.Avatar
{

    public class AvatarComponent : BaseComponent
    {
        private void Awake()
        {
            Debug.Log(Time.timeScale);
        }
    }
}