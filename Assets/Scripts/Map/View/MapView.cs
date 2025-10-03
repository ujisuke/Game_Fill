using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Assets.Scripts.Map.View
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private StageDescriptionData stageDescriptionData;
        [SerializeField] private Sprite frontOpenInitSprite;
        [SerializeField] private Sprite frontOpenFromTitleInitSprite;
        [SerializeField] private ViewData viewData;
        [SerializeField] private ImageView frontView;
        [SerializeField] private ImageView rightArrowView;
        [SerializeField] private ImageView leftArrowView;
        [SerializeField] private ImageView mailView;
        [SerializeField] private Text mailText;
        [SerializeField] private int mailTitleSize;
        [SerializeField] private Text easyText;
        [SerializeField] private Text normalText;
        [SerializeField] private Text hardText;
        [SerializeField] private Animator NiLLAnimator;
        [SerializeField] private Animator BoLLAnimator;
        [SerializeField] private SpriteRenderer hardIcon;
        private Color32 easyColor, normalColor, hardColor;

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

        public void Initialize(int stageIndex, bool isStageIndexUpper, bool isStageIndexLower, bool isEasyMode, bool isHardMode)
        {
            easyColor = viewData.EasyModeColor;
            normalColor = viewData.NormalModeColor;
            hardColor = viewData.HardModeColor;

            if (isStageIndexUpper && isStageIndexLower)
            {
                rightArrowView.PlayAnim("Empty");
                leftArrowView.PlayAnim("Empty");
            }
            else if (isStageIndexUpper)
            {
                rightArrowView.PlayAnim("Empty");
                leftArrowView.PlayAnim("Awake");
            }
            else if (isStageIndexLower)
            {
                rightArrowView.PlayAnim("Awake");
                leftArrowView.PlayAnim("Empty");
            }
            else
            {
                rightArrowView.PlayAnim("Awake");
                leftArrowView.PlayAnim("Awake");
            }

            UpdateMailText(stageIndex);
            UpdateHardIcon(stageIndex);
            UpdateTextAndCharacterOnDifficulty(isEasyMode, isHardMode);
        }

        public async UniTask SelectRight(int stageIndex, bool isStageIndexUpper, CancellationToken token)
        {
            rightArrowView.PlayAnim("Awake");
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            rightArrowView.PlayAnim("Selected");
            leftArrowView.PlayAnim("Awake");
            mailView.PlayAnim("ChangeText");
            UpdateMailText(stageIndex);
            UpdateHardIcon(stageIndex);

            if (!isStageIndexUpper)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            rightArrowView.PlayAnim("Empty");
        }

        public async UniTask SelectLeft(int stageIndex, bool isStageIndexLower, CancellationToken token)
        {
            leftArrowView.PlayAnim("Awake");
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            leftArrowView.PlayAnim("Selected");
            rightArrowView.PlayAnim("Awake");
            mailView.PlayAnim("ChangeText");
            UpdateMailText(stageIndex);
            UpdateHardIcon(stageIndex);

            if (!isStageIndexLower)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            leftArrowView.PlayAnim("Empty");
        }

        private void UpdateMailText(int stageIndex)
        {
            if (LocalizationSettings.SelectedLocale.Identifier.Code == "ja")
                mailText.text = $"<size={mailTitleSize}>{stageDescriptionData.GetStageNameJA(stageIndex)}</size>\n\n" + stageDescriptionData.GetDescriptionJA(stageIndex);
            else
                mailText.text = $"<size={mailTitleSize}>{stageDescriptionData.GetStageNameEN(stageIndex)}</size>\n\n" + stageDescriptionData.GetDescriptionEN(stageIndex);
        }

        private void UpdateHardIcon(int stageIndex)
        {
            hardIcon.enabled = ES3.Load("Hard" + stageIndex, false);
        }

        private void UpdateMailOnDifficulty(bool isEasyMode, bool isHardMode)
        {
            if (isEasyMode)
                mailView.PlayAnim("GetEasy");
            else if (isHardMode)
                mailView.PlayAnim("GetHard");
            else
                mailView.PlayAnim("GetNormal");
        }

        private void UpdateTextAndCharacterOnDifficulty(bool isEasyMode, bool isHardMode)
        {
            easyText.color = isEasyMode ? new Color32(easyColor.r, easyColor.g, easyColor.b, 255) : new Color32(easyColor.r, easyColor.g, easyColor.b, viewData.TextHideAlpha);
            normalText.color = !isEasyMode && !isHardMode ? new Color32(normalColor.r, normalColor.g, normalColor.b, 255) : new Color32(normalColor.r, normalColor.g, normalColor.b, viewData.TextHideAlpha);
            hardText.color = isHardMode ? new Color32(hardColor.r, hardColor.g, hardColor.b, 255) : new Color32(hardColor.r, hardColor.g, hardColor.b, viewData.TextHideAlpha);

            if (isEasyMode)
            {
                NiLLAnimator.Play("IdleEasy");
                BoLLAnimator.Play("IdleEasy");
            }
            else if (isHardMode)
            {
                NiLLAnimator.Play("IdleHard");
                BoLLAnimator.Play("IdleHard");
            }
            else
            {
                NiLLAnimator.Play("IdleNormal");
                BoLLAnimator.Play("IdleNormal");
            }
        }

        public async UniTask SetDifficulty(bool isEasyMode, bool isHardMode, CancellationToken token)
        {
            mailView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            UpdateTextAndCharacterOnDifficulty(isEasyMode, isHardMode);
            UpdateMailOnDifficulty(isEasyMode, isHardMode);
        }
    }
}
