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

        public string CurrentStageName => mapModel.CurrentStageName;
        public string TitleSceneName => sceneNameData.TitleSceneName;

        private void Awake()
        {
            mapModel = new MapModel(sceneNameData);
            mapStateMachine = new MapStateMachine(this);
            AudioSourceView.Instance.PlayMapBGM();
        }

        private void Update()
        {
            mapStateMachine.HandleInput();
        }

        public void InitializeMail()
        {
            mapView.InitializeMailAndIcon(MapModel.CurrentStageIndex, mapModel.IsStageIndexUpper, mapModel.IsStageIndexLower);
        }

        public void SelectRight(CancellationToken token)
        {
            mapModel.UpdateStageIndex(1, out bool isChanged);
            if (!isChanged)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            mapView.SelectRight(MapModel.CurrentStageIndex, mapModel.IsStageIndexUpper, token).Forget();
        }

        public void SelectLeft(CancellationToken token)
        {
            mapModel.UpdateStageIndex(-1, out bool isChanged);
            if (!isChanged)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            mapView.SelectLeft(MapModel.CurrentStageIndex, mapModel.IsStageIndexLower, token).Forget();
        }

        public async UniTask SetDifficulty(bool isHard, CancellationToken token)
        {
            if (MapModel.IsHardMode == isHard)
                return;
            MapModel.SetDifficulty(isHard);
            AudioSourceView.Instance.PlayChooseSE();
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
    }
}
