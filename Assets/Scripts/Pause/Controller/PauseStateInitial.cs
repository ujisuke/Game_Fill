using Assets.Scripts.Common.Controller;
using Assets.Scripts.Volume.Controller;
using Unity.Mathematics;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseStateInitial : IPauseState
    {
        private readonly PauseController pC;
        private readonly PauseStateMachine pSM;
        private int selectedIndex;
        private static bool isBack;
        private static bool doesSelectStage;
        private static bool doesExitGame;
        public static bool IsBack => isBack;
        public static bool DoesSelectStage => doesSelectStage;
        public static bool DoesExitGame => doesExitGame;

        public PauseStateInitial(PauseController pC, PauseStateMachine pSM)
        {
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {
            selectedIndex = 0;
            pC.Initialize();
            VolumeStateExitPage.ResetFlag();
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetUpKeyWithCooldown())
            {
                selectedIndex = math.max(0, selectedIndex - 1);
                pC.UpdateInitButtonSelection(selectedIndex);
            }
            else if (CustomInputSystem.Instance.GetDownKeyWithCooldown())
            {
                selectedIndex = math.min(3, selectedIndex + 1);
                pC.UpdateInitButtonSelection(selectedIndex);
            }

            if (CustomInputSystem.Instance.GetPauseKeyWithCooldown())
            {
                isBack = true;
                pSM.ChangeState(new PauseStateSelected(pC));
            }
            else if (CustomInputSystem.Instance.DoesSelectKeyUp())
            {
                switch (selectedIndex)
                {
                    case 0:
                        isBack = true;
                        pSM.ChangeState(new PauseStateSelected(pC));
                        break;
                    case 1:
                        pSM.ChangeState(new PauseStateSetVolume(pC));
                        break;
                    case 2:
                        doesSelectStage = true;
                        pSM.ChangeState(new PauseStateSelected(pC));
                        break;
                    case 3:
                        doesExitGame = true;
                        pSM.ChangeState(new PauseStateSelected(pC));
                        break;
                }
            }
        }

        public void OnStateExit()
        {

        }

        public static void ResetFlags()
        {
            isBack = false;
            doesSelectStage = false;
            doesExitGame = false;
        }
    }
}
