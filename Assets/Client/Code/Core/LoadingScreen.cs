using System;
using System.Collections;
using UnityEngine;

namespace Client.Code.Core
{
    public class LoadingScreen : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        public float FadeDuration;
        private Coroutine _hideAnimation;
        private Coroutine _showAnimation;

        public void Show(bool force = false)
        {
            if (_hideAnimation != null)
                StopCoroutine(_hideAnimation);
            gameObject.SetActive(true);
            _showAnimation = StartCoroutine(PlayFade(0, 1, force ? 0 : FadeDuration));
        }

        public void Hide()
        {
            if (_showAnimation != null)
                StopCoroutine(_showAnimation);
            _hideAnimation = StartCoroutine(PlayFade(1, 0, FadeDuration, () => gameObject.SetActive(false)));
        }

        private IEnumerator PlayFade(float from, float to, float duration, Action endAction = null)
        {
            yield return null; // possible heavy operations in this frame

            var time = 0f;
            if (duration > 0)
            {
                var speed = (to - from) / duration;
                while (time < duration)
                {
                    CanvasGroup.alpha += speed * Time.deltaTime;
                    time += Time.deltaTime;
                    yield return null;
                }
            }

            CanvasGroup.alpha = to;
            endAction?.Invoke();
        }
    }
}