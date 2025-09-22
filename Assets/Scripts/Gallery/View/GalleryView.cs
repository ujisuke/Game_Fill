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

namespace Assets.Scripts.Gallery.View
{
    public class GalleryView : MonoBehaviour
    {
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
        [SerializeField] private Text hardText;
        [SerializeField] private Text switchText;
        [SerializeField] private Text entrustText;
        private int stageChildIndexPrev = 0;

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

        public void Initialize(int stageIndex, int stageChildIndex, bool isHardMode, int stageIndexUpper, bool isStageIndexUpper, bool isStageIndexLower)
        {
            stageChildIndexPrev = stageChildIndex;
            if (stageIndexUpper < 0)
            {
                leftArrowView.PlayAnim("Empty");
                rightArrowView.PlayAnim("Empty");
                stageText.text = "まだ ナニも ない...";
                hardText.enabled = false;
                switchText.enabled = false;
                entrustText.enabled = false;
                for (int i = 0; i < StageBoardViewList.Count; i++)
                    StageBoardViewList[i].gameObject.SetActive(false);
                return;
            }

            SetStageBoards(stageIndex);
            StageBoardViewList[stageChildIndex].PlayAnim("Selected");
            SetDifficulty(isHardMode);

            if (isStageIndexUpper && isStageIndexLower)
            {
                leftArrowView.PlayAnim("Empty");
                rightArrowView.PlayAnim("Empty");
                stageText.text = "チュートリアル";
            }
            else if (isStageIndexLower)
            {
                leftArrowView.PlayAnim("Empty");
                rightArrowView.PlayAnim("Awake");
                stageText.text = "チュートリアル";
            }
            else if (isStageIndexUpper)
            {
                leftArrowView.PlayAnim("Awake");
                rightArrowView.PlayAnim("Empty");
                stageText.text = $"ステージ {stageIndex}";
            }
            else
            {
                leftArrowView.PlayAnim("Awake");
                rightArrowView.PlayAnim("Awake");
                stageText.text = $"ステージ {stageIndex}";
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
            stageText.text = stageIndex == 0 ? "チュートリアル" : $"ステージ {stageIndex}";

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
            stageText.text = stageIndex == 0 ? "チュートリアル" : $"ステージ {stageIndex}";
            if (!isStageIndexLower)
                return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            leftArrowView.PlayAnim("Empty");
        }

        public void SetDifficulty(bool isHard)
        {
            hardText.enabled = isHard;
        }
    }

    [Serializable]
    class StagePhotos
    {
        [SerializeField] private List<Sprite> photoList;
        public List<Sprite> PhotoList => photoList;
    }
}
