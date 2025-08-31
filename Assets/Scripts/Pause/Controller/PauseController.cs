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

        private void Awake()
        {
            pSM = new PauseStateMachine(this);
        }

        private void Update()
        {
            pSM.HandleInput();
        }

        public void UpdateInitButtonSelection(int index)
        {
            pauseView.UpdateInitButtonSelection(index);
        }

        public void SetActiveButtons(bool isActive)
        {
            pauseView.SetActiveButtons(isActive);
        }
    }
}
