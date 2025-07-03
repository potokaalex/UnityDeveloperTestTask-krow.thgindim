using Client.Code.Gameplay;
using UnityEngine;

namespace Client.Code.Core
{
    public class ToCameraRotator : MonoBehaviour
    {
        private CameraController _cameraController;

        public void Construct(CameraController cameraController) => _cameraController = cameraController;

        public void LateUpdate() => transform.LookAt(transform.position + _cameraController.transform.forward, _cameraController.transform.up);
    }
}