using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Title.View
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private List<ButtonView> buttonList;
        [SerializeField] private ViewData viewData;
        [SerializeField] private ImageView frontView;
        [SerializeField] private Sprite frontOpenFromMapInitSprite;

        private void Awake()
        {
            Cursor.visible = false;
            buttonList[0].PlaySelectedAnim();
        }

        public void UpdateInitButtonSelection(int indexNew, int indexPrev)
        {
            buttonList[indexPrev].PlayDeselectedAnim();
            buttonList[indexNew].PlaySelectedAnim();
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

        public async UniTask CloseWithBlack(CancellationToken token)
        {
            frontView.PlayAnim("InBlack", viewData.InBlackAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneWithBlackDelaySeconds), cancellationToken: token);
        }

        public async UniTask CloseToTutorial(CancellationToken token)
        {
            frontView.PlayAnim("Close", viewData.CloseAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadTutorialDelaySeconds), cancellationToken: token);
        }
    }
}
