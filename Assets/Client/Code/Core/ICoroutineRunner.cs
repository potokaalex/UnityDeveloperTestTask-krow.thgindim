using System;
using System.Collections;
using Client.Code.Core.Dispose;
using UnityEngine;

namespace Client.Code.Core
{
    public class CoroutineRunner
    {
        private readonly MonoBehaviour _mb;

        public CoroutineRunner(MonoBehaviour mb) => _mb = mb;

        public IDisposable StartCoroutine(IEnumerator enumerator)
        {
            _mb.StartCoroutine(enumerator);
            return new DisposableAction<IEnumerator>(StopCoroutine, enumerator);
        }

        public void StopCoroutine(IEnumerator enumerator) => _mb.StopCoroutine(enumerator);
    }
}