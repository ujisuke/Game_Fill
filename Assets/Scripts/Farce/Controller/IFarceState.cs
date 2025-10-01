namespace Assets.Scripts.Farce.Controller
{
    public interface IFarceState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
