using UnityEditor;
using UnityEngine;

namespace Source.Scripts.Core.Common
{
    public class GizmoPoint : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private Color textColor;
        public Vector3 size = new Vector3(1, 1, 1);

        private GUIStyle guiStyle;

        void OnDrawGizmos()
        {
            guiStyle = new GUIStyle();
            guiStyle.normal.textColor = textColor;
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position, size);
            Handles.Label(transform.position, gameObject.name, guiStyle);
        }
    }
}