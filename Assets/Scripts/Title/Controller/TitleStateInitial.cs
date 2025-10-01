using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Unity.Mathematics;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateInitial : ITitleState
    {
        private readonly TitleController tC;
        private readonly TitleStateMachine tSM;
        private int selectedIndex;
        private readonly bool isFromSetVolume;

        public TitleStateInitial(TitleController tC, TitleStateMachine tSM, bool isFromSetVolume = false)
        {
            this.tC = tC;
            this.tSM = tSM;
            this.isFromSetVolume = isFromSetVolume;
            this.selectedIndex = isFromSetVolume ? 1 : 0;
        }

        public void OnStateEnter()
        {
            tC.SetActiveButtons(true);
            tC.UpdateInitButtonSelection(selectedIndex, selectedIndex, isFromSetVolume);
            tC.OpenScene();
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetUpKeyWithCooldown())
            {
                int selectedIndexNew = math.max(0, selectedIndex - 1);
                tC.UpdateInitButtonSelection(selectedIndexNew, selectedIndex);
                selectedIndex = selectedIndexNew;
            }
            else if (CustomInputSystem.Instance.GetDownKeyWithCooldown())
            {
                int selectedIndexNew = math.min(3, selectedIndex + 1);
                tC.UpdateInitButtonSelection(selectedIndexNew, selectedIndex);
                selectedIndex = selectedIndexNew;
            }

            else if (CustomInputSystem.Instance.GetSelectKeyUp())
            {
                AudioSourceView.Instance.PlayChooseSE();
                switch (selectedIndex)
                {
                    case 0:
                        if (ES3.Load("ClearedStageIndex", -1) == -1)
                            tSM.ChangeState(new TitleStateLoadTutorial(tC));
                        else
                            tSM.ChangeState(new TitleStateLoadMap(tC));
                        break;
                    case 1:
                        tSM.ChangeState(new TitleStateSetVolume(tC, tSM));
                        tC.SetActiveButtons(false);
                        break;
                    case 2:
                        tSM.ChangeState(new TitleStateLoadGallery(tC));
                        break;
                    case 3:
                        tSM.ChangeState(new TitleStateExitGame());
                        break;
                }
            }
        }

        public void OnStateExit()
        {

        }
    }
}
