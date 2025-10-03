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

namespace Assets.Scripts.Gallery.View
{
    public class GalleryView : MonoBehaviour
    {
        [SerializeField] private StageDescriptionData stageDescriptionData;
        [SerializeField] private Sprite frontOpenInitSprite;
        [SerializeField] private Sprite frontOpenFromTitleInitSprite;
        [SerializeField] private ViewData viewData;
        [SerializeField] private ImageView frontView;
        [SerializeField] private List<ImageView> StageBoardViewList;
        [SerializeField] private List<StagePhotos> StagePhotosList;
        [SerializeField] private List<Image> StageImageList;
        [SerializeField] private Sprite NullPhoto;
        [SerializeField] private ImageView rightArrowView;
        [SerializeField] private ImageView leftArrowView;
        [SerializeField] private Text stageText;
        [SerializeField] private Text easyText;
        [SerializeField] private Text normalText;
        [SerializeField] private Text hardText;
        [SerializeField] private Text switchText;
        [SerializeField] private Text entrustText;
        [SerializeField] private string noneTextJA;
        [SerializeField] private string noneTextEN;
        private int stageChildIndexPrev = 0;
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

        public void Initialize(int stageIndex, int stageChildIndex, bool isEasyMode, bool isHardMode, int stageIndexUpper, bool isStageIndexUpper, bool isStageIndexLower)
        {
            easyColor = viewData.EasyModeColor;
            normalColor = viewData.NormalModeColor;
            hardColor = viewData.HardModeColor;
            stageChildIndexPrev = stageChildIndex;
            if (stageIndexUpper < 0)
            {
                leftArrowView.PlayAnim("Empty");
                rightArrowView.PlayAnim("Empty");
                if (LocalizationSettings.SelectedLocale.Identifier.Code == "ja")
                    stageText.text = noneTextJA;
                else
                    stageText.text = noneTextEN;
                easyText.enabled = false;
                normalText.enabled = false;
                hardText.enabled = false;
                switchText.enabled = false;
                entrustText.enabled = false;
                for (int i = 0; i < StageBoardViewList.Count; i++)
                    StageBoardViewList[i].gameObject.SetActive(false);
                return;
            }

            SetStageBoards(stageIndex);
            StageBoardViewList[stageChildIndex].PlayAnim("Selected");
            SetDifficulty(isEasyMode, isHardMode);

            if (LocalizationSettings.SelectedLocale.Identifier.Code == "ja")
                stageText.text = stageDescriptionData.GetStageNameJA(stageIndex);
            else
                stageText.text = stageDescriptionData.GetStageNameEN(stageIndex);
            if (isStageIndexUpper && isStageIndexLower)
            {
                leftArrowView.PlayAnim("Empty");
                rightArrowView.PlayAnim("Empty");
            }
            else if (isStageIndexLower)
            {
                leftArrowView.PlayAnim("Empty");
                rightArrowView.PlayAnim("Awake");
            }
            else if (isStageIndexUpper)
            {
                leftArrowView.PlayAnim("Awake");
                rightArrowView.PlayAnim("Empty");
            }
            else
            {
                leftArrowView.PlayAnim("Awake");
                rightArrowView.PlayAnim("Awake");
            }
        }

        public void SetStageBoards(int stageIndex)
        {
            for (int i = 0; i < StageImageList.Count; i++)
                StageImageList[i].sprite = StagePhotosList[stageIndex].PhotoList[i];
        }

        public void SelectRightChild(int stageChildIndex)
        {
            StageBoardViewList[stageChildIndexPrev].PlayAnim("DeSelected");
            StageBoardViewList[stageChildIndex].PlayAnim("Selected");
            stageChildIndexPrev = stageChildIndex;
        }

        public async UniTask SelectRightStage(int stageIndex, bool isStageIndexUpper, CancellationToken token)
        {
            rightArrowView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            rightArrowView.PlayAnim("Selected");
            leftArrowView.PlayAnim("Awake");
            SetStageBoards(stageIndex);
            if (LocalizationSettings.SelectedLocale.Identifier.Code == "ja")
                stageText.text = stageDescriptionData.GetStageNameJA(stageIndex);
            else
                stageText.text = stageDescriptionData.GetStageNameEN(stageIndex);

            if (!isStageIndexUpper)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            rightArrowView.PlayAnim("Empty");
        }

        public void SelectLeftChild(int stageChildIndex)
        {
            StageBoardViewList[stageChildIndexPrev].PlayAnim("DeSelected");
            StageBoardViewList[stageChildIndex].PlayAnim("Selected");
            stageChildIndexPrev = stageChildIndex;
        }

        public async UniTask SelectLeftStage(int stageIndex, bool isStageIndexLower, CancellationToken token)
        {
            leftArrowView.PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            leftArrowView.PlayAnim("Selected");
            rightArrowView.PlayAnim("Awake");
            SetStageBoards(stageIndex);
            if (LocalizationSettings.SelectedLocale.Identifier.Code == "ja")
                stageText.text = stageDescriptionData.GetStageNameJA(stageIndex);
            else
                stageText.text = stageDescriptionData.GetStageNameEN(stageIndex);
            if (!isStageIndexLower)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            leftArrowView.PlayAnim("Empty");
        }

        public void SetDifficulty(bool isEasyMode, bool isHardMode)
        {
            easyText.color = isEasyMode ? new Color32(easyColor.r, easyColor.g, easyColor.b, 255) : new Color32(easyColor.r, easyColor.g, easyColor.b, viewData.TextHideAlpha);
            normalText.color = !isEasyMode && !isHardMode ? new Color32(normalColor.r, normalColor.g, normalColor.b, 255) : new Color32(normalColor.r, normalColor.g, normalColor.b, viewData.TextHideAlpha);
            hardText.color = isHardMode ? new Color32(hardColor.r, hardColor.g, hardColor.b, 255) : new Color32(hardColor.r, hardColor.g, hardColor.b, viewData.TextHideAlpha);
        }
    }

    [Serializable]
    class StagePhotos
    {
        [SerializeField] private List<Sprite> photoList;
        public List<Sprite> PhotoList => photoList;
    }
}
