namespace Assets.Scripts.Map.Controller
{
    public class MapStateInitial : IMapState
    {
        private readonly MapStateMachine mSM;
        private readonly MapController mC;

        public MapStateInitial(MapStateMachine stateMachine, MapController mC)
        {
            mSM = stateMachine;
            this.mC = mC;
        }

        public void OnStateEnter()
        {
            mC.InitializeMail();
            mC.OpenStage();
        }

        public void HandleInput()
        {
            mSM.ChangeState(new MapStateSelectStage(mSM, mC));
        }

        public void OnStateExit()
        {

        }
    }
}
