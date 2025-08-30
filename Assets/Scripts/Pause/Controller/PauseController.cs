using Assets.Scripts.Common.Controller;
using Assets.Scripts.Pause.View;
using Assets.Scripts.Stage.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private PauseView pauseView;
        private PauseStateMachine pSM;

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

        public void UpdateVolumeButtonSelection(int index)
        {
            pauseView.UpdateVolumeButtonSelection(index);
        }

        public void SetActiveVolumePage(bool isActive)
        {
            pauseView.SetActiveVolumePage(isActive);
        }
    }
}
