using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "alive_item", menuName = "GameAssets/Alive Item", order = 0)]
    public class AliveItem : BaseConfig
    {
        public float hp;
    }
}