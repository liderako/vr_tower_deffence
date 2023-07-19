using System.Collections;
using Source.Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Source.Scripts.ZXRCore.Avatar
{
    public class GrabHandPoseComponent : BaseComponent
    {
        public float poseTransitionDuration = 0.2f;
        public HandDataComponent rightHandPose;
        public HandDataComponent leftHandPose;

        private XRGrabInteractable grabInteractable;
        private Vector3 startHandPosition;
        private Vector3 endHandPosition;
        private Quaternion startHandRotation;
        private Quaternion endHandRotation;

        private Quaternion[] startFingerRotations;
        private Quaternion[] endFingerRotations;

        private void Awake()
        {
            InitComponentInGameObject(out grabInteractable);
            grabInteractable.selectEntered.AddListener(SetupPose);
            grabInteractable.selectExited.AddListener(UnSetupPose);
            rightHandPose.gameObject.SetActive(false);
            leftHandPose.gameObject.SetActive(false);
        }

        private void SetupPose(BaseInteractionEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
            {
                HandDataComponent handDataComponent = args.interactorObject.transform.GetComponentInChildren<HandDataComponent>();
                handDataComponent.animator.enabled = false;
                if (handDataComponent.type == HandDataComponent.HandModelType.Right)
                {
                    SetHandDataValues(handDataComponent, rightHandPose);
                }
                else
                {
                    SetHandDataValues(handDataComponent, leftHandPose);
                }

                StartCoroutine(SetHandDataRoutine(handDataComponent,
                    endHandPosition,
                    endHandRotation,
                    endFingerRotations,
                    startHandPosition,
                    startHandRotation,
                    startFingerRotations));
            }
        }

        private void UnSetupPose(BaseInteractionEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
            {
                HandDataComponent handDataComponent = args.interactorObject.transform.GetComponentInChildren<HandDataComponent>();
                if (handDataComponent.animator != null)
                {
                    handDataComponent.animator.enabled = true;
                }

                StartCoroutine(SetHandDataRoutine(handDataComponent,
                    startHandPosition,
                    startHandRotation,
                    startFingerRotations,
                    endHandPosition,
                    endHandRotation,
                    endFingerRotations));
            }
        }

        private void SetHandDataValues(HandDataComponent h1, HandDataComponent h2)
        {
            startHandPosition = new Vector3(
                h1.root.localPosition.x / h1.root.localScale.x,
                h1.root.localPosition.y / h1.root.localScale.y,
                h1.root.localPosition.z / h1.root.localScale.z);
            endHandPosition = new Vector3(
                h2.root.localPosition.x / h2.root.localScale.x,
                h2.root.localPosition.y / h2.root.localScale.y,
                h2.root.localPosition.z / h2.root.localScale.z);
            startHandRotation = h1.root.localRotation;
            endHandRotation = h2.root.localRotation;
            startFingerRotations = new Quaternion[h1.fingerBones.Length];
            endFingerRotations = new Quaternion[h1.fingerBones.Length];

            for (int i = 0; i < h1.fingerBones.Length; i++)
            {
                startFingerRotations[i] = h1.fingerBones[i].localRotation;
                endFingerRotations[i] = h2.fingerBones[i].localRotation;
            }
        }

        private void SetHandData(HandDataComponent h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
        {
            h.root.localPosition = newPosition;
            h.root.localRotation = newRotation;
            for (int i = 0; i < h.fingerBones.Length; i++)
            {
                h.fingerBones[i].localRotation = newBonesRotation[i];
            }
        }

        public IEnumerator SetHandDataRoutine(HandDataComponent h, Vector3 newPosition, Quaternion newRotation,
            Quaternion[] newBonesRotation, Vector3 startingPosition, Quaternion startingRotation,
            Quaternion[] startingBonesRotation)
        {
            float timer = Time.deltaTime;

            while (timer < poseTransitionDuration)
            {
                Vector3 p = Vector3.Lerp(startingPosition, newPosition, timer / poseTransitionDuration);
                Quaternion r = Quaternion.Lerp(startingRotation, newRotation, (timer / poseTransitionDuration) + 0.01f);

                h.root.localPosition = p;
                h.root.localRotation = r;
                for (int i = 0; i < newBonesRotation.Length; i++)
                {
                    h.fingerBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i],
                        timer / poseTransitionDuration);
                }

                timer += Time.deltaTime;
                yield return null;
            }
        }
#if UNITY_EDITOR
        [MenuItem("Tools/Mirror Selected Right Grab Pose")]
        public static void MirrorRightPose()
        {
            Debug.Log("MIRROR RIGHT POSE");
            GrabHandPoseComponent handPose = Selection.activeGameObject.GetComponent<GrabHandPoseComponent>();
            handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
        }

#endif

        public void MirrorPose(HandDataComponent poseToMirror, HandDataComponent poseUsedToMirror)
        {
            Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
            mirroredPosition.x *= -1;

            Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
            mirroredQuaternion.y *= -1;
            mirroredQuaternion.z *= -1;

            poseToMirror.root.localPosition = mirroredPosition;
            poseToMirror.root.localRotation = mirroredQuaternion;

            for (int i = 0; i < poseUsedToMirror.fingerBones.Length; i++)
            {
                poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
            }
        }
    }
}