using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Gallery.Model;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Map.Model;
using Assets.Scripts.Stage.Model;
using Assets.Scripts.Stage.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Stage.Controller
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        private StageModel stageModel;
        [SerializeField] private StageView stageView;
        [SerializeField] private int timeLimitNormal;
        [SerializeField] private int timeLimitHard;
        private StageStateMachine stageStateMachine;
        [SerializeField] private bool isFinalStage;
        [SerializeField] private bool isEndingStage;
        [SerializeField] private string nextSceneName;
        [SerializeField] private SceneNameData sceneNameData;
        private static bool isInGallery = false;
        private bool isHardMode => MapModel.IsHardMode || GalleryModel.IsHardMode;

        public bool IsFinalStage => isFinalStage;
        public bool IsEndingStage => isEndingStage;
        public string NextSceneName => nextSceneName;
        public string MapSceneName => sceneNameData.MapSceneName;
        public string PauseSceneName => sceneNameData.PauseSceneName;
        public string TitleSceneName => sceneNameData.TitleSceneName;
        public string GallerySceneName => sceneNameData.GallerySceneName;
        public static bool IsInGallery => isInGallery;

        private void Awake()
        {
            int timeLimit = isHardMode ? timeLimitHard : timeLimitNormal;
            stageModel = new StageModel(tilemap, this, timeLimit);
            stageStateMachine = new(this);
            stageView.SetBlockMap(tilemap);
            stageView.SetTimeLimit(timeLimit, isHardMode);
            if (!isEndingStage)
                AudioSourceView.Instance.PlayStageBGM(isInGallery ? GalleryModel.CurrentStageIndex : MapModel.CurrentStageIndex);
        }

        private void Update()
        {
            stageStateMachine.Update();
        }

        public void PlaySlowEffect()
        {
            stageView.PlaySlowEffect();
        }

        public void StopSlowEffect()
        {
            stageView.StopSlowEffect();
        }

        public async UniTask PlayClearEffect(CancellationToken token)
        {
            await stageView.PlayClearEffect(token);
        }

        public async UniTask PlayClearFinalEffect(CancellationToken token)
        {
            await stageView.PlayClearFinalEffect(token);
        }

        public async UniTask PlayEndingEffect(CancellationToken token)
        {
            await stageView.PlayEndingEffect(token);
        }

        public async UniTask CloseStage(bool isRetry, CancellationToken token)
        {
            if (isRetry)
                await stageView.CloseStageRetry(token);
            else
                await stageView.CloseStage(token);
        }

        public async UniTask CloseStageWithBlack(CancellationToken token)
        {
            await stageView.CloseStageWithBlack(token);
        }

        public void OpenStage()
        {
            stageView.OpenStage();
        }

        public void SetTimeLimit(int timeLimit)
        {
            stageView.SetTimeLimit(timeLimit, isHardMode);
        }

        public static void SetIsOnGallery(bool isOnGallery)
        {
            isInGallery = isOnGallery;
        }
    }
}
