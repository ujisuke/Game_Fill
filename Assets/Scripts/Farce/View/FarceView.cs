using System;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Farce.View
{
    public class FarceView : MonoBehaviour
    {
        [SerializeField] private ViewData viewData;
        [SerializeField] private ImageView frontView;
        [SerializeField] private Sprite frontOutBlackInitSprite;

        public void Open()
        {
            frontView.PlayAnim("OutBlack", viewData.OutBlackAnimSeconds);
            frontView.Initialize(frontOutBlackInitSprite);
        }

        public async UniTask Close(CancellationToken token)
        {
            frontView.PlayAnim("Close", viewData.CloseAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneDelaySeconds - viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
            AudioSourceView.Instance.PlayCloseSE();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
        }
    }
}
