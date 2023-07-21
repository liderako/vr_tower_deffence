using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Source.Core.Interactable.BowAndArrow
{
    public class PullMeasurer : XRBaseInteractable
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        public float PullAmount { get; private set; } = 0.0f;

        public Vector3 PullPosition => Vector3.Lerp(start.position, end.position, PullAmount);

        private Vector3 originPosition;
        
        private void Start()
        {
            originPosition = transform.localPosition;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            PullAmount = 0;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);
            if (isSelected)
            {
                // Update pull values while the measurer is grabbed
                if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
                {
                    UpdatePull();
                }
            }
            else if (!isSelected)
            {
                transform.position = start.position;
                transform.localPosition = originPosition;
            }
        }

        private void UpdatePull()
        {
            // Figure out the new pull value, and it's position in space
            PullAmount = CalculatePull(firstInteractorSelecting.transform.position);
        }

        private float CalculatePull(Vector3 pullPosition)
        {
            // Direction, and length
            Vector3 pullDirection = pullPosition - start.position;
            Vector3 targetDirection = end.position - start.position;

            // Figure out out the pull direction
            float maxLength = targetDirection.magnitude;
            targetDirection.Normalize();
            
            float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
            return Mathf.Clamp(pullValue, 0.0f, 1.0f);
        }

        private void OnDrawGizmos()
        {
            // Draw line from start to end point
            if (start && end)
            {
                Gizmos.DrawLine(start.position, end.position);   
            }
        }
    }
}
