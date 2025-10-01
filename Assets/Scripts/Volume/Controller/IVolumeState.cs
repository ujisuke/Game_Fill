namespace Assets.Scripts.Volume.Controller
{
    public interface IVolumeState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
