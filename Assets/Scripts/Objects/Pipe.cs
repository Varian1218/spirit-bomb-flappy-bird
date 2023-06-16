using UnityEngine;

namespace Objects
{
    public class Pipe : MonoBehaviour
    {
        [SerializeField] private Vector2 size;
        public Vector3 Position => transform.position;
        public Vector2 Size => size;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, size);
        }
#endif
    }
}