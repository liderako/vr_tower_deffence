using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "enemy_item", menuName = "GameAssets/Enemy Items", order = 0)]
    public class EnemyItem : BaseConfig
    {
        public float minSpeed;
        [Range(0, 10)] public float speed;
    }
}