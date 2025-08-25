namespace Assets.Scripts.Map.Controller
{
    public class MapStateInitial : IMapState
    {
        private readonly MapStateMachine mSM;

        public MapStateInitial(MapStateMachine stateMachine)
        {
            mSM = stateMachine;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            mSM.ChangeState(new MapStateSelectStage(mSM));
        }

        public void OnStateExit()
        {

        }
    }
}
