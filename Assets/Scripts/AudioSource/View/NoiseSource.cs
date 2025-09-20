using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AudioSource.View
{
    public class NoiseSource : MonoBehaviour
    {
        [SerializeField] private UnityEngine.AudioSource audioSource;
        [SerializeField] private float noiseSeconds;

        private void Awake()
        {
            var token = this.GetCancellationTokenOnDestroy();
            Play(token).Forget();
        }

        private async UniTask Play(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(noiseSeconds), cancellationToken: token);
            audioSource.Stop();
        }
    }
}
