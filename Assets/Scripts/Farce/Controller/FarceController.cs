using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Farce.View;
using Assets.Scripts.Common.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Farce.Controller
{
    public class FarceController : MonoBehaviour
    {
        [SerializeField] private FarceView farceView;
        [SerializeField] private SceneNameData sceneNameData;
        [SerializeField] private bool isEnding;
        private FarceStateMachine fSM;
        public bool IsEnding => isEnding;
        public string VolumeSceneName => sceneNameData.VolumeSceneName;
        public string SelectStageSceneName => sceneNameData.MapSceneName;
        public string PauseSceneName => sceneNameData.PauseSceneName;
        public string EndingSceneName => sceneNameData.EndingSceneName;

        private void Awake()
        {
            fSM = new FarceStateMachine(this);
            AudioSourceView.Instance.PlayFarceBGM();
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
