using Assets.Scripts.Common.Controller;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Pause.View;
using Assets.Scripts.Stage.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private PauseView pauseView;
        [SerializeField] private SceneNameData sceneNameData;
        private PauseStateMachine pSM;
        public string VolumeSceneName => sceneNameData.VolumeSceneName;
        public string PauseSceneName => sceneNameData.PauseSceneName;

        private void Awake()
        {
            pSM = new PauseStateMachine(this);
        }

        private void Update()
        {
            pSM.HandleInput();
        }

        public void Initialize()
        {
            pauseView.Initialize();
        }

        public void UpdateInitButtonSelection(int index)
        {
            pauseView.UpdateInitButtonSelection(index);
        }
    }
}
