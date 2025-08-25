using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateLoadScene : IMapState
    {
        private readonly MapStateMachine mSM;

        public MapStateLoadScene(MapStateMachine stateMachine)
        {
            mSM = stateMachine;
        }

        public void OnStateEnter()
        {
            SceneManager.LoadScene(mSM.StageNameData.CurrentStageName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
