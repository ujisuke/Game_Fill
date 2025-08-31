using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private List<MailText> mailTextList;
        private int mailIndexPrev;
        private CancellationTokenSource cTS;
        private CancellationToken token;

        private void Awake()
        {
            mailIndexPrev = 0;
            cTS = new();
            token = cTS.Token;
        }

        public async UniTask CloseScene()
        {
            frontView.PlayAnim("Close", viewData.CloseAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneDelaySeconds), cancellationToken: token);
        }

        public async UniTask CloseSceneToTitle()
        {
            frontView.PlayAnim("CloseWithTitle", viewData.CloseWithTitleAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneWithTitleDelaySeconds), cancellationToken: token);
        }

        public void OpenSceneFromStage()
        {
            frontView.PlayAnim("Open", viewData.OpenAnimSeconds);
            frontView.Initialize(frontOpenInitSprite);
        }

        public void OpenSceneFromTitle()
        {
            frontView.PlayAnim("OpenWithTitle", viewData.OpenWithTitleAnimSeconds);
            frontView.Initialize(frontOpenFromTitleInitSprite);
        }

        public void InitializeMail(int stageIndex)
        {
            mailIndexPrev = stageIndex;
            if (stageIndex >= mailTextList.Count - 1)
                rightArrowView.PlayAnim("Empty");
            if (stageIndex <= 0)
                leftArrowView.PlayAnim("Empty");
            UpdateMailText(stageIndex);
        }

        public async UniTask SelectRight(int stageIndex)
        {
            if (stageIndex == mailTextList.Count - 1 && mailIndexPrev == mailTextList.Count - 1)
                return;
            rightArrowView.PlayAnim("Awake");
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            rightArrowView.PlayAnim("Select");
            leftArrowView.PlayAnim("Awake");
            mailView.PlayAnim("ChangeText");
            UpdateMailText(stageIndex);
            mailIndexPrev = stageIndex;
            if (stageIndex < mailTextList.Count - 1)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            rightArrowView.PlayAnim("Empty");
        }

        public async UniTask SelectLeft(int stageIndex)
        {
            if (stageIndex == 0 && mailIndexPrev == 0)
                return;
            leftArrowView.PlayAnim("Awake");
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            leftArrowView.PlayAnim("Select");
            rightArrowView.PlayAnim("Awake");
            mailView.PlayAnim("ChangeText");
            UpdateMailText(stageIndex);
            mailIndexPrev = stageIndex;
            if (stageIndex > 0)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            leftArrowView.PlayAnim("Empty");
        }

        private void UpdateMailText(int stageIndex)
        {
            mailText.text = mailTextList[stageIndex].GetText(mailText.fontSize);
        }

        public void OnDestroy()
        {
            cTS?.Cancel();
        }
    }

    [Serializable]
    class MailText
    {
        [SerializeField] private string title;
        [SerializeField, TextArea(3, 10)] private string mainText;

        public string GetText(int mainTextSize)
        {
            int titleSize = mainTextSize * 2;
            string titleString = $"<size={titleSize}>{title}</size>\n\n";
            return titleString + mainText;
        }
    }
}
