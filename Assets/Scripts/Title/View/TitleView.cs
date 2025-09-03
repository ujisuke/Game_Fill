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
        [SerializeField] private List<ButtonView> buttonList;
        [SerializeField] private ViewData viewData;
        [SerializeField] private ImageView frontView;
        [SerializeField] private Sprite frontOpenFromMapInitSprite;
        private int currentIndex;

        private void Awake()
        {
            currentIndex = 0;
            buttonList[currentIndex].PlaySelectedAnim();
        }

        public void UpdateInitButtonSelection(int index)
        {
            if (currentIndex == index) return;

            buttonList[currentIndex].PlayDeselectedAnim();
            currentIndex = index;
            buttonList[currentIndex].PlaySelectedAnim();
        }

        public void SetActiveButtons(bool isActive)
        {
            foreach (var button in buttonList)
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
