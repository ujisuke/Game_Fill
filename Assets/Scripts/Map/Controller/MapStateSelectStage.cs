using System.Threading;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Map.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateSelectStage : IMapState
    {
        private readonly MapStateMachine mSM;
        private readonly MapController mC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public MapStateSelectStage(MapStateMachine stateMachine, MapController mC)
        {
            mSM = stateMachine;
            this.mC = mC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetRightKeyWithCooldown())
            {
                mC.UpdateStageIndex(1);
                mC.SelectRight(token);
            }
            else if (CustomInputSystem.Instance.GetLeftKeyWithCooldown())
            {
                mC.UpdateStageIndex(-1);
                mC.SelectLeft(token);
            }
            else if (CustomInputSystem.Instance.GetUpKeyWithCooldown())
            {
                mC.SetDifficulty(true, token).Forget();
            }
            else if (CustomInputSystem.Instance.GetDownKeyWithCooldown())
            {
                mC.SetDifficulty(false, token).Forget();
            }

            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MapStateLoadStage(mC));
            else if (CustomInputSystem.Instance.GetPauseKeyWithCooldown())
                mSM.ChangeState(new MapStateLoadTitle(mC));
        }

        public void OnStateExit()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}
