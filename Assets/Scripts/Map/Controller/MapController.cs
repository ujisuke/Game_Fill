using System.Threading;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Map.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private MapView mapView;
        [SerializeField] private SceneNameData sceneNameData;
        private MapStateMachine mapStateMachine;

        public string CurrentStageName => sceneNameData.CurrentStageName;
        public string PauseSceneName => sceneNameData.PauseSceneName;
        public string TitleSceneName => sceneNameData.TitleSceneName;

        private void Awake()
        {
            mapStateMachine = new MapStateMachine(this);
        }

        private void Update()
        {
            mapStateMachine.HandleInput();
        }

        public void InitializeMail()
        {
            mapView.InitializeMail(SceneNameData.CurrentStageIndex);
        }

        public void SelectRight(int stageIndex, CancellationToken token)
        {
            mapView.SelectRight(stageIndex, token).Forget();
        }

        public void SelectLeft(int stageIndex, CancellationToken token)
        {
            mapView.SelectLeft(stageIndex, token).Forget();
        }

        public void Entrust()
        {
            mapView.Entrust();
        }

        public async UniTask CloseScene(CancellationToken token)
        {
            await mapView.CloseScene(token);
        }

        public async UniTask CloseSceneToTitle(CancellationToken token)
        {
            await mapView.CloseSceneToTitle(token);
        }

        public void OpenSceneNotFromTitle()
        {
            mapView.OpenSceneNotFromTitle();
        }

        public void OpenSceneFromTitle()
        {
            mapView.OpenSceneFromTitle();
        }

        public void UpdateCurrentStageName(int direction)
        {
            sceneNameData.UpdateCurrentStageName(direction);
        }
    }
}
