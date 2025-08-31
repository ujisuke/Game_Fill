using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Title.View
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private List<ImageView> initButtonList;
        [SerializeField] private ViewData viewData;
        [SerializeField] private ImageView frontView;
        [SerializeField] private Sprite frontOpenFromMapInitSprite;

        private void Awake()
        {
            initButtonList[0].PlayAnim("Selected");
            for (int i = 1; i < initButtonList.Count; i++)
                initButtonList[i].PlayAnim("DeSelected");
        }

        public void UpdateInitButtonSelection(int index)
        {
            for (int i = 0; i < initButtonList.Count; i++)
            {
                if (i == index)
                    initButtonList[i].PlayAnim("Selected");
                else
                    initButtonList[i].PlayAnim("DeSelected");
            }
        }

        public void SetActiveButtons(bool isActive)
        {
            foreach (var button in initButtonList)
                button.gameObject.SetActive(isActive);
        }

        public void Open()
        {
            frontView.PlayAnim("OutBlack", viewData.OutBlackAnimSeconds);
            frontView.Initialize(frontOpenFromMapInitSprite);
        }

        public async UniTask Close(CancellationToken token)
        {
            frontView.PlayAnim("InBlack", viewData.InBlackAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneWithBlackDelaySeconds), cancellationToken: token);
        }
    }
}
