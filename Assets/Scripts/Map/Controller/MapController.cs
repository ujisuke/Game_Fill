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

        public void SelectRight(int stageIndex)
        {
            mapView.SelectRight(stageIndex).Forget();
        }

        public void SelectLeft(int stageIndex)
        {
            mapView.SelectLeft(stageIndex).Forget();
        }

        public void CloseStage()
        {
            mapView.CloseStage();
        }

        public void OpenStage()
        {
            mapView.OpenStage();
        }

        public void UpdateCurrentStageName(int direction)
        {
            sceneNameData.UpdateCurrentStageName(direction);
        }

        public void OnDestroy()
        {
            mapView.OnDestroy();
        }
    }
}
