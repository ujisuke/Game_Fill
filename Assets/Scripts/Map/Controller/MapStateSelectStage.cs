using Assets.Scripts.Common.Controller;
using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateSelectStage : IMapState
    {
        private readonly MapStateMachine mSM;

        public MapStateSelectStage(MapStateMachine stateMachine)
        {
            mSM = stateMachine;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetRightKeyWithCooldown())
                mSM.StageNameData.UpdateCurrentStageName(true);
            else if (CustomInputSystem.Instance.GetLeftKeyWithCooldown())
                mSM.StageNameData.UpdateCurrentStageName(false);
            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MapStateLoadScene(mSM));
        }

        public void OnStateExit()
        {

        }
    }
}
