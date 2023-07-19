using Source.Scripts.ZXRCore.Avatar;
using Unity.XR.CoreUtils;
using UnityEngine;
using Zenject;

namespace Source.ZXRCore.Avatar
{
    public class CharacterSpawnComponent : MonoBehaviour
    {
        [Inject]
        private void Init(AvatarComponent avatarComponent)
        {
            avatarComponent.GetComponent<XROrigin>().MoveCameraToWorldLocation(transform.position);
        }
    }
}