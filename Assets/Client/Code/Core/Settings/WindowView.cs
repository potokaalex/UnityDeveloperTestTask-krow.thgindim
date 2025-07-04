using UnityEngine;

namespace Client.Code.Services.UI.Window
{
    public abstract class WindowView : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        public bool IsOpen;

        public virtual void Initialize()
        {
            CanvasGroup.alpha = IsOpen ? 1 : 0;
            CanvasGroup.blocksRaycasts = IsOpen;
        }

        public virtual void Open()
        {
            if (!IsOpen)
            {
                IsOpen = true;
                CanvasGroup.alpha = 1;
                CanvasGroup.blocksRaycasts = true;
            }
        }

        public virtual void Close()
        {
            if (IsOpen)
            {
                IsOpen = false;
                CanvasGroup.alpha = 0;
                CanvasGroup.blocksRaycasts = false;
            }
        }
    }
}