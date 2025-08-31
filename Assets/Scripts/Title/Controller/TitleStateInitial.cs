using Assets.Scripts.Common.Controller;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateInitial : ITitleState
    {
        private readonly TitleController tC;
        private readonly TitleStateMachine tSM;
        private int selectedIndex;
        private static bool doesSelectStage;
        private static bool doesExitGame;
        public static bool DoesSelectStage => doesSelectStage;
        public static bool DoesExitGame => doesExitGame;

        public TitleStateInitial(TitleController tC, TitleStateMachine tSM)
        {
            this.tC = tC;
            this.tSM = tSM;
        }

        public void OnStateEnter()
        {
            selectedIndex = 0;
            tC.SetActiveButtons(true);
            tC.UpdateInitButtonSelection(selectedIndex);
            tC.OpenScene();
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetLeftKeyWithCooldown())
            {
                selectedIndex = math.max(0, selectedIndex - 1);
                tC.UpdateInitButtonSelection(selectedIndex);
            }
            else if (CustomInputSystem.Instance.GetRightKeyWithCooldown())
            {
                selectedIndex = math.min(2, selectedIndex + 1);
                tC.UpdateInitButtonSelection(selectedIndex);
            }

            else if (CustomInputSystem.Instance.DoesSelectKeyUp())
            {
                switch (selectedIndex)
                {
                    case 0:
                        doesSelectStage = true;
                        tSM.ChangeState(new TitleStateLoadMap(tC));
                        break;
                    case 1:
                        tSM.ChangeState(new TitleStateSetVolume(tC, tSM));
                        tC.SetActiveButtons(false);
                        break;
                    case 2:
                        doesExitGame = true;
                        tSM.ChangeState(new TitleStateExitGame());
                        break;
                }
            }
        }

        public void OnStateExit()
        {

        }

        public static void ResetFlags()
        {
            doesSelectStage = false;
            doesExitGame = false;
        }
    }
}
