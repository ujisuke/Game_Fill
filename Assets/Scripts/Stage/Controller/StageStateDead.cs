using Assets.Scripts.Stage.Model;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateDead : IStageState
    {
        private readonly StageStateMachine sSM;

        public StageStateDead(StageStateMachine sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            StageModel.Instance.DestroyAllBlock();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}