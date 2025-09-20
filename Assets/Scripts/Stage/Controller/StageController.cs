using System.Threading;
using Assets.Scripts.AudioSource.View;
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

        public bool IsFinalStage => isFinalStage;
        public bool IsEndingStage => isEndingStage;
        public string NextSceneName => nextSceneName;
        public string MapSceneName => sceneNameData.MapSceneName;
        public string PauseSceneName => sceneNameData.PauseSceneName;
        public string TitleSceneName => sceneNameData.TitleSceneName;

        private void Awake()
        {
            int timeLimit = MapModel.IsHardMode ? timeLimitHard : timeLimitNormal;
            stageModel = new StageModel(tilemap, this, timeLimit);
            stageStateMachine = new(this);
            stageView.SetBlockMap(tilemap);
            stageView.SetTimeLimit(timeLimit);
            if(!isEndingStage)
                AudioSourceView.Instance.PlayStageBGM(MapModel.CurrentStageIndex);
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
            stageView.SetTimeLimit(timeLimit);
        }
    }
}
