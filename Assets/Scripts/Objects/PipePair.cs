using UnityEngine;

namespace Objects
{
    public class PipePair : MonoBehaviour
    {
        [SerializeField] private Pipe[] pipes;
        [SerializeField] private Vector2 point;
        public Pipe[] Pipes => pipes;
        public Vector2 Point => point;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public float PositionX => transform.position.x;

        public float PositionY
        {
            get => transform.position.y;
            set
            {
                var position = transform.position;
                position.y = value;
                transform.position = position;
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, point);
        }
#endif
    }
}