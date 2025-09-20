using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Map.Model;
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
        private MapModel mapModel;
        private int stageIndexPrev;

        public string CurrentStageName => mapModel.CurrentStageName;
        public string TitleSceneName => sceneNameData.TitleSceneName;

        private void Awake()
        {
            mapStateMachine = new MapStateMachine(this);
            mapModel = new MapModel(sceneNameData);
            AudioSourceView.Instance.PlayMapBGM();
        }

        private void Update()
        {
            mapStateMachine.HandleInput();
        }

        public void InitializeMail()
        {
            mapView.InitializeMailAndIcon(MapModel.CurrentStageIndex);
        }

        public void SelectRight(CancellationToken token)
        {
            if (stageIndexPrev == MapModel.CurrentStageIndex) return;
            AudioSourceView.Instance.PlaySelectSE();
            mapView.SelectRight(MapModel.CurrentStageIndex, token).Forget();
        }

        public void SelectLeft(CancellationToken token)
        {
            if (stageIndexPrev == MapModel.CurrentStageIndex) return;
            AudioSourceView.Instance.PlaySelectSE();
            mapView.SelectLeft(MapModel.CurrentStageIndex, token).Forget();
        }

        public async UniTask SetDifficulty(bool isHard, CancellationToken token)
        {
            MapModel.SetDifficulty(isHard);
            await mapView.SetDifficulty(isHard, token);
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

        public void UpdateStageIndex(int additionalIndex)
        {
            stageIndexPrev = MapModel.CurrentStageIndex;
            mapModel.UpdateStageIndex(additionalIndex);
        }
    }
}
