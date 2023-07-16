using System;
using UniRx;
using UnityEngine;

namespace Source.Scripts.Core.Interactable
{
    public class Bullet : MonoBehaviour
    {
        public void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(_ => DeactivateBullet()).AddTo(this);
        }

        public void OnTriggerEnter(Collider other)
        {
            DeactivateBullet();
        }

        private void DeactivateBullet()
        {
            Destroy(gameObject); // todo change to return pool
        }
    }
}