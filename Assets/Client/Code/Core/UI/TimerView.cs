using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Core.UI
{
    public class TimerView : MonoBehaviour
    {
        public Slider Slider;

        public void View(float max, float current)
        {
            Slider.maxValue = max;
            Slider.value = current;
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}