using Assets.Scripts.Player.Model;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateBorn : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;

        public PlayerStateBorn(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            pSM.ChangeState(new PlayerStateMove(pM, pC, pSM));
        }

        public void OnStateExit()
        {

        }
    }
}