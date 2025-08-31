using System.Threading;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Pause.View;
using Assets.Scripts.Stage.Controller;
using Assets.Scripts.Title.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Title.Controller
{
    public class TitleController : MonoBehaviour
    {
        [SerializeField] private TitleView titleView;
        [SerializeField] private SceneNameData sceneNameData;
        private TitleStateMachine tSM;
        public string VolumeSceneName => sceneNameData.VolumeSceneName;
        public string SelectStageSceneName => sceneNameData.MapSceneName;

        private void Awake()
        {
            tSM = new TitleStateMachine(this);
        }

        private void Update()
        {
            tSM.HandleInput();
        }

        public void UpdateInitButtonSelection(int index)
        {
            titleView.UpdateInitButtonSelection(index);
        }

        public void SetActiveButtons(bool isActive)
        {
            titleView.SetActiveButtons(isActive);
        }

        public void OpenScene()
        {
            titleView.Open();
        }

        public async UniTask CloseScene(CancellationToken token)
        {
            await titleView.Close(token);
        }
    }
}
