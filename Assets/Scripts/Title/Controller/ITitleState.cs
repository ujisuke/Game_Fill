namespace Assets.Scripts.Title.Controller
{
    public interface ITitleState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
