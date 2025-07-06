using Client.Code.Core.LifeTime.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.Code.Gameplay.Player
{
    public class PlayerRaycaster : ITickable
    {
        private readonly CameraController _cameraController;

        public PlayerRaycaster(CameraController cameraController) => _cameraController = cameraController;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var ray = _cameraController.Camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out var hit))
                    if (hit.transform && hit.transform.TryGetComponent<IPlayerInteractive>(out var interactive))
                        interactive.Interact();
            }
        }
    }
}