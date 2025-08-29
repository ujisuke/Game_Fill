using Assets.Scripts.Map.Data;
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
        [SerializeField] private int timeLimit;
        private StageStateMachine stageStateMachine;
        [SerializeField] private bool isFinalStage;
        [SerializeField] private string nextStageName;
        [SerializeField] private SceneNameData sceneNameData;

        public bool IsFinalStage => isFinalStage;
        public string NextStageName => nextStageName;
        public string MapSceneName => sceneNameData.MapSceneName;

        private void Awake()
        {
            stageModel = new StageModel(tilemap, this, timeLimit);
            stageStateMachine = new(this);
            stageView.SetBlockMap(tilemap);
            stageView.SetTimeLimit(timeLimit);
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

        public async UniTask PlayClearEffect()
        {
            await stageView.PlayClearEffect();
        }

        public void CloseStage(bool isRetry)
        {
            stageView.CloseStage(isRetry);
        }

        public void OpenStage()
        {
            stageView.OpenStage();
        }

        public void SetTimeLimit(int timeLimit)
        {
            stageView.SetTimeLimit(timeLimit);
        }

        public void OnDestroy()
        {
            stageView.OnDestroy();
        }
    }
}
