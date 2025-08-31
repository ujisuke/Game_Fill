using System.Threading;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Farce.View;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Pause.View;
using Assets.Scripts.Stage.Controller;
using Assets.Scripts.Title.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Farce.Controller
{
    public class FarceController : MonoBehaviour
    {
        [SerializeField] private FarceView farceView;
        [SerializeField] private SceneNameData sceneNameData;
        private FarceStateMachine fSM;
        public string VolumeSceneName => sceneNameData.VolumeSceneName;
        public string SelectStageSceneName => sceneNameData.MapSceneName;
        public string PauseSceneName => sceneNameData.PauseSceneName;

        private void Awake()
        {
            fSM = new FarceStateMachine(this);
        }

        private void Update()
        {
            fSM.HandleInput();
        }

        public void OpenScene()
        {
            farceView.Open();
        }

        public async UniTask CloseScene(CancellationToken token)
        {
            await farceView.Close(token);
        }
    }
}
