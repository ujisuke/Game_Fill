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
        [SerializeField] private float showDelaySeconds = 0f;

        private void Awake()
        {
            Play().Forget();
        }

        private async UniTask Play()
        {
            var token = this.GetCancellationTokenOnDestroy();
            text.text = string.Empty;
            await UniTask.Delay(TimeSpan.FromSeconds(showDelaySeconds), cancellationToken: token);
            for (int k = 0; k < showText.Count; k++)
            {
                string currentText = string.Empty;
                float showCharSeconds = viewData.ShowCharSeconds;
                float showSentenceSeconds = viewData.ShowSentenceSeconds;
                string colorTextL = string.Empty;
                string colorTextR = string.Empty;
                int i = 0;
                while (i < showText[k].Length)
                {
                    if (showText[k][i] == '<' && colorTextL == string.Empty)
                    {
                        colorTextL = showText[k].Substring(i, 15);
                        colorTextR = "</color>";
                        i += 15;
                        continue;
                    }
                    else if (showText[k][i] == '<' && colorTextL != string.Empty)
                    {
                        colorTextL = string.Empty;
                        colorTextR = string.Empty;
                        i += 8;
                        continue;
                    }

                    currentText += colorTextL + showText[k][i] + colorTextR;
                    text.text = currentText;
                    if (showText[k][i] == '\n')
                        await UniTask.Delay(TimeSpan.FromSeconds(showCharSeconds * 10), cancellationToken: token);
                    else
                        await UniTask.Delay(TimeSpan.FromSeconds(showCharSeconds), cancellationToken: token);
                    i++;
                }
                await UniTask.Delay(TimeSpan.FromSeconds(showSentenceSeconds), cancellationToken: token);
            }
        }

        public void ShowAndPlay()
        {
            text.enabled = true;
            Play().Forget();
        }
    }
}