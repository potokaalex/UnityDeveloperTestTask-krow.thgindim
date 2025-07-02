using UnityEngine;

namespace Client.Code.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        public Camera Camera;
        public float PositionVelocity;
        public Vector2 PositionMin;
        public Vector2 PositionMax;
        public float ZoomVelocity;
        public float ZoomMin;
        public float ZoomMax;
        private float _scrollDelta;
        private Vector2 _positionDirection;

        public void Update() => WriteInputs();

        private void WriteInputs()
        {
            WritePositionInput();
            _scrollDelta = Input.mouseScrollDelta.y;
        }

        public void LateUpdate()
        {
            Zoom();
            MovePosition();
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            var areaCenter = new Vector3(PositionMin.x + PositionMax.x, 0f, PositionMin.y + PositionMax.y) / 2;
            var areaSize = new Vector3(PositionMax.x - PositionMin.x, 0f, PositionMax.y - PositionMin.y);
            Gizmos.DrawWireCube(areaCenter, areaSize);
        }

        private void WritePositionInput()
        {
            _positionDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
                _positionDirection += Vector2.left;
            if (Input.GetKey(KeyCode.D))
                _positionDirection += Vector2.right;
            if (Input.GetKey(KeyCode.W))
                _positionDirection += Vector2.up;
            if (Input.GetKey(KeyCode.S))
                _positionDirection += Vector2.down;

            _positionDirection.Normalize();
        }

        private void MovePosition()
        {
            if (_positionDirection.magnitude > 0)
            {
                var rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                var direction = rotation * new Vector3(_positionDirection.x, 0, _positionDirection.y);
                var target = transform.position + direction * (PositionVelocity * Time.deltaTime);
                target.x = Mathf.Clamp(target.x, PositionMin.x, PositionMax.x);
                target.z = Mathf.Clamp(target.z, PositionMin.y, PositionMax.y);
                transform.position = target;
            }
        }

        private void Zoom()
        {
            if (Mathf.Abs(_scrollDelta) > 0)
            {
                var target = Camera.orthographicSize - _scrollDelta * ZoomVelocity * Time.deltaTime;
                target = Mathf.Clamp(target, ZoomMin, ZoomMax);
                Camera.orthographicSize = target;
            }
        }
    }
}