using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Common.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common.View
{
    public class TextBox : MonoBehaviour
    {
        [SerializeField] private ViewData viewData;
        [SerializeField] private Text text;
        [SerializeField, TextArea(3, 10)] private List<string> showText;

        private void Awake()
        {
            Play().Forget();
        }

        private async UniTask Play()
        {
            var token = this.GetCancellationTokenOnDestroy();
            for (int k = 0; k < showText.Count; k++)
            {
                string currentText = string.Empty;
                int colorCount = 0;
                float showCharSeconds = viewData.ShowCharSeconds;
                for (int i = 0; i <= showText[k].Length; i++)
                {
                    currentText += showText[k][i];
                    if (showText[k][i] == '<' || showText[k][i] == '>')
                        colorCount++;
                    if (colorCount == 0)
                    {
                        text.text = currentText;
                        await UniTask.Delay(TimeSpan.FromSeconds(showCharSeconds), cancellationToken: token);
                    }
                    else if (colorCount == 4)
                    {
                        text.text = currentText;
                        colorCount = 0;
                        await UniTask.Delay(TimeSpan.FromSeconds(showCharSeconds), cancellationToken: token);
                    }
                }
            }
        }
    }
}