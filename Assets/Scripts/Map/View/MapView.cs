using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Assets.Scripts.Map.View
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private Sprite frontOpenInitSprite;
        [SerializeField] private Sprite frontOpenFromTitleInitSprite;
        [SerializeField] private ViewData viewData;
        [SerializeField] private ImageView frontView;
        [SerializeField] private ImageView rightArrowView;
        [SerializeField] private ImageView leftArrowView;
        [SerializeField] private ImageView mailView;
        [SerializeField] private Text mailText;
        [SerializeField] private int mailTitleSize;
        [SerializeField] private List<MailText> mailTextList;
        [SerializeField] private Text hardText;
        [SerializeField] private Animator NiLLAnimator;
        [SerializeField] private Animator BoLLAnimator;
        [SerializeField] private SpriteRenderer hardIcon;
        private int mailIndexPrev;

        private void Awake()
        {
            mailIndexPrev = 0;
        }

        public async UniTask CloseScene(CancellationToken token)
        {
            frontView.PlayAnim("Close", viewData.CloseAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneDelaySeconds - viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
            AudioSourceView.Instance.PlayCloseSE();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
        }

        public async UniTask CloseSceneToTitle(CancellationToken token)
        {
            frontView.PlayAnim("InBlack", viewData.InBlackAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneWithBlackDelaySeconds), cancellationToken: token);
        }

        public void OpenSceneNotFromTitle()
        {
            frontView.PlayAnim("Open", viewData.OpenAnimSeconds);
            frontView.Initialize(frontOpenInitSprite);
        }

        public void OpenSceneFromTitle()
        {
            frontView.PlayAnim("OutBlack", viewData.OutBlackAnimSeconds);
            frontView.Initialize(frontOpenFromTitleInitSprite);
        }

        public void InitializeMailAndIcon(int stageIndex)
        {
            int listUpper = math.min(ES3.Load("ClearedStageIndex", 0) + 1, mailTextList.Count - 1);
            mailIndexPrev = stageIndex;
            if (stageIndex < listUpper)
                rightArrowView.PlayAnim("Awake");
            if (stageIndex > 0)
                leftArrowView.PlayAnim("Awake");
            UpdateMailText(stageIndex);
            UpdateHardIcon(stageIndex);
        }

        public async UniTask SelectRight(int stageIndex, CancellationToken token)
        {
            int listUpper = math.min(ES3.Load("ClearedStageIndex", 0) + 1, mailTextList.Count - 1);
            if (stageIndex == listUpper && mailIndexPrev == listUpper)
                return;
            rightArrowView.PlayAnim("Awake");
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            rightArrowView.PlayAnim("Selected");
            leftArrowView.PlayAnim("Awake");
            mailView.PlayAnim("ChangeText");
            UpdateMailText(stageIndex);
            UpdateHardIcon(stageIndex);
            mailIndexPrev = stageIndex;
            if (stageIndex < listUpper)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            rightArrowView.PlayAnim("Empty");
        }

        public async UniTask SelectLeft(int stageIndex, CancellationToken token)
        {
            if (stageIndex == 0 && mailIndexPrev == 0)
                return;
            leftArrowView.PlayAnim("Awake");
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            leftArrowView.PlayAnim("Selected");
            rightArrowView.PlayAnim("Awake");
            mailView.PlayAnim("ChangeText");
            UpdateMailText(stageIndex);
            UpdateHardIcon(stageIndex);
            mailIndexPrev = stageIndex;
            if (stageIndex > 0)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            leftArrowView.PlayAnim("Empty");
        }

        private void UpdateMailText(int stageIndex)
        {
            mailText.text = mailTextList[stageIndex].GetText(mailTitleSize);
        }

        private void UpdateHardIcon(int stageIndex)
        {
            hardIcon.enabled = ES3.Load("Hard" + stageIndex, false);
        }

        public async UniTask SetDifficulty(bool isHard, CancellationToken token)
        {
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            hardText.enabled = isHard;
            if (isHard)
            {
                mailView.PlayAnim("GetHard");
                NiLLAnimator.Play("IdleHard");
                BoLLAnimator.Play("IdleHard");
            }
            else
            {
                NiLLAnimator.Play("Idle");
                BoLLAnimator.Play("Idle");
            }
        }
    }

    [Serializable]
    class MailText
    {
        [SerializeField, TextArea] private string title;
        [SerializeField, TextArea(3, 10)] private string mainText;

        public string GetText(int titleSize)
        {
            string titleString = $"<size={titleSize}>{title}</size>\n\n";
            return titleString + mainText;
        }
    }
}
