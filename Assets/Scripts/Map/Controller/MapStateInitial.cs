using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateInitial : IMapState
    {
        private readonly MapStateMachine mSM;
        private readonly MapController mC;
        private static bool isFromTitle;

        public MapStateInitial(MapStateMachine stateMachine, MapController mC)
        {
            mSM = stateMachine;
            this.mC = mC;
        }

        public void OnStateEnter()
        {
            mC.InitializeMail();
            if (isFromTitle)
                mC.OpenSceneFromTitle();
            else
                mC.OpenSceneFromStage();
            isFromTitle = false;
        }

        public void HandleInput()
        {
            mSM.ChangeState(new MapStateSelectStage(mSM, mC));
        }

        public void OnStateExit()
        {

        }

        public static void OpenFromTitle()
        {
            isFromTitle = true;
        }
    }
}
