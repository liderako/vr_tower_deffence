using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "enemy_item", menuName = "GameAssets/Enemy Item", order = 0)]
    public class EnemyItem : BaseConfig
    {
        public float minSpeed;
        [Range(0, 10)] public float speed; 
        public AliveItem aliveItem;
        public float defaultDamage;
    }
}