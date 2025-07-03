using Client.Code.Core.Rx;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Core.UI
{
    public class ButtonView : MonoBehaviour
    {
        public Button Button;

        public EventAction OnClick { get; } = new();

        public void Reset() => TryGetComponent(out Button);

        public void Awake() => Button.onClick.AddListener(OnClick.Invoke);

        public void OnDestroy() => Button.onClick.RemoveListener(OnClick.Invoke);
    }
}