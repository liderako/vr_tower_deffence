using UnityEngine;

namespace Source.Scripts.Entities
{
    public class DamagableComponent : MonoBehaviour
    {
        [field:SerializeField] public float Damage { get; private set; }
    }
}