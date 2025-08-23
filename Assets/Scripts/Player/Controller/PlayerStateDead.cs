using Assets.Scripts.Player.Model;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateDead : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;

        public PlayerStateDead(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {
            pC.OnDestroy();
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}