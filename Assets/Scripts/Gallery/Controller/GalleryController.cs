using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Gallery.Model;
using Assets.Scripts.Gallery.View;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Gallery.Controller
{
    public class GalleryController : MonoBehaviour
    {
        [SerializeField] private GalleryView galleryView;
        [SerializeField] private SceneNameData sceneNameData;
        private GalleryStateMachine galleryStateMachine;
        private GalleryModel galleryModel;

        public string CurrentStageName => galleryModel.CurrentStageName;
        public string TitleSceneName => sceneNameData.TitleSceneName;

        private void Awake()
        {
            galleryModel = new GalleryModel(sceneNameData);
            galleryStateMachine = new GalleryStateMachine(this);
        }

        private void Start()
        {
            AudioSourceView.Instance.PlayMapBGM();
        }

        private void Update()
        {
            galleryStateMachine.HandleInput();
        }

        public void Initialize()
        {
            if (StageController.IsInGallery == false)
                galleryModel.SetCurrentStageAndChildIndexFromTitle();
            else
                StageController.SetIsOnGallery(false);

            galleryView.Initialize(GalleryModel.CurrentStageIndex, GalleryModel.CurrentStageChildIndex, GalleryModel.IsHardMode, GalleryModel.StageIndexUpper, GalleryModel.IsStageIndexUpper, GalleryModel.IsStageIndexLower);
        }

        public async UniTask CloseScene(CancellationToken token)
        {
            await galleryView.CloseScene(token);
        }

        public async UniTask CloseSceneToTitle(CancellationToken token)
        {
            await galleryView.CloseSceneToTitle(token);
        }

        public void OpenSceneNotFromTitle()
        {
            galleryView.OpenSceneNotFromTitle();
        }

        public void OpenSceneFromTitle()
        {
            galleryView.OpenSceneFromTitle();
        }

        public void SelectRight(CancellationToken token)
        {
            galleryModel.UpdateStageAndChildIndex(1, out bool isStageChildIndexChanged, out bool isStageIndexChanged);

            if (!isStageChildIndexChanged)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            galleryView.SelectRightChild(GalleryModel.CurrentStageChildIndex);

            if (isStageIndexChanged)
                galleryView.SelectRightStage(GalleryModel.CurrentStageIndex, GalleryModel.IsStageIndexUpper, token).Forget();
        }

        public void SelectLeft(CancellationToken token)
        {
            galleryModel.UpdateStageAndChildIndex(-1, out bool isStageChildIndexChanged, out bool isStageIndexChanged);

            if (!isStageChildIndexChanged)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            galleryView.SelectLeftChild(GalleryModel.CurrentStageChildIndex);

            if (isStageIndexChanged)
                galleryView.SelectLeftStage(GalleryModel.CurrentStageIndex, GalleryModel.IsStageIndexLower, token).Forget();
        }

        public void SetDifficulty(bool isHard)
        {
            if (GalleryModel.IsHardMode == isHard)
                return;
            GalleryModel.SetDifficulty(isHard);
            AudioSourceView.Instance.PlayChooseSE();
            galleryView.SetDifficulty(isHard);
        }
    }
}
