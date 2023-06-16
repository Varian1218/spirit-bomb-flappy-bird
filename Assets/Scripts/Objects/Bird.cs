using UnityEngine;

namespace Objects
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] private float gravityScale = .6f;
        [SerializeField] private float radius;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

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

        public float Radius => radius;

        public Vector3 Velocity { private get; set; }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
        public void Step(float dt)
        {
            Velocity += gravityScale * dt * Physics.gravity;
            transform.position += Velocity * dt;
        }
    }
}