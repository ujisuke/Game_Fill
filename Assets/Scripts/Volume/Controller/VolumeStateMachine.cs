namespace Assets.Scripts.Volume.Controller
{
    public class VolumeStateMachine
    {
        private IVolumeState currentState;

        public VolumeStateMachine(VolumeController vC)
        {
            currentState = new VolumeStateInitial(vC, this);
            currentState.OnStateEnter();
        }

        public void ChangeState(IVolumeState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }
    }
}
