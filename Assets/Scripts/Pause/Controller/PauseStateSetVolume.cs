using Assets.Scripts.Common.Controller;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseStateSetVolume : IPauseState
    {
        private readonly PauseController pC;
        private readonly PauseStateMachine pSM;
        private int selectedIndex;

        public PauseStateSetVolume(PauseController pC, PauseStateMachine pSM)
        {
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {
            selectedIndex = 0;
            pC.SetActiveVolumePage(true);
            pC.UpdateVolumeButtonSelection(selectedIndex);
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetLeftKeyWithCooldown())
            {
                selectedIndex = math.max(0, selectedIndex - 1);
                pC.UpdateVolumeButtonSelection(selectedIndex);
            }
            else if (CustomInputSystem.Instance.GetRightKeyWithCooldown())
            {
                selectedIndex = math.min(3, selectedIndex + 1);
                pC.UpdateVolumeButtonSelection(selectedIndex);
            }

            if (CustomInputSystem.Instance.GetPauseKeyWithCooldown()
            || (CustomInputSystem.Instance.DoesSelectKeyUp() && selectedIndex == 3))
                pSM.ChangeState(new PauseStateInitial(pC, pSM));
        }

        public void OnStateExit()
        {
            pC.SetActiveVolumePage(false);
        }
    }
}
