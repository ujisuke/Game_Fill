using Assets.Scripts.Common.Controller;
using Assets.Scripts.Map.Data;
using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateSelectStage : IMapState
    {
        private readonly MapStateMachine mSM;
        private readonly MapController mC;

        public MapStateSelectStage(MapStateMachine stateMachine, MapController mC)
        {
            mSM = stateMachine;
            this.mC = mC;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetRightKeyWithCooldown())
            {
                mC.SceneNameData.UpdateCurrentStageName(1);
                mC.SelectRight(SceneNameData.CurrentStageIndex);
            }
            else if (CustomInputSystem.Instance.GetLeftKeyWithCooldown())
            {
                mC.SceneNameData.UpdateCurrentStageName(-1);
                mC.SelectLeft(SceneNameData.CurrentStageIndex);
            }
            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MapStateLoadScene(mSM, mC));
        }

        public void OnStateExit()
        {

        }
    }
}
