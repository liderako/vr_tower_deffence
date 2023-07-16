using UnityEngine;

namespace Source.Scripts.ZXRCore.Avatar
{
    public class HandDataComponent : MonoBehaviour
    {
        public enum HandModelType { Left, Right }

        public HandModelType type;
        public Transform root;
        public Animator animator;
        public Transform[] fingerBones;
    }
}