namespace Assets.Scripts.Player.Controller
{
    public interface IPlayerState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
