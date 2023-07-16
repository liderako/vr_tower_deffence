using System;
using Source.Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Source.Scripts.Core.Interactable
{
    public class Weapon : BaseComponent, IInteractableActivateListener
    {
        //TODO need create settings for different weapons
        
        [SerializeField] private GameObject bulletDemo;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float speed;
        [SerializeField] private AudioClip clip;
        
        private AudioSource source;

        private void Start()
        {
            InitComponentInGameObject(out source);
        }

        public void Interact(ActivateEventArgs args)
        {
            GameObject bullet = GetBullet();
            bullet.transform.position = spawnPoint.position;
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * speed;
            source.PlayOneShot(clip);
        }

        public GameObject GetBullet()
        {
            return Instantiate(bulletDemo); // todo change logic for bullets on object pool or smth another
        }
    }
}