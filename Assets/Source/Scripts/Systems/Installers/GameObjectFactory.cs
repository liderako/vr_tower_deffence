using UnityEngine;
using Zenject;

namespace Game.Installers.Factory
{
    // It's zenject factory to create some game object in realtime (with inject logic)
    public class GameObjectFactory
    {
        private readonly DiContainer diContainer;

        public GameObjectFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }
        
        public GameObject Create(GameObject prefab, Vector3 at)
        {
            return diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }
        
        public GameObject Create(GameObject prefab, Transform parent)
        {
            return diContainer.InstantiatePrefab(prefab, parent);
        }
    }
}