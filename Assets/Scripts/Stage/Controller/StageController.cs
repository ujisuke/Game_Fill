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

        private void Awake()
        {
            stageModel = new StageModel(tilemap, this);
            stageView.SetBlockMap(tilemap);
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

        public void OnDestroy()
        {
            stageView.OnDestroy();
        }
    }
}
