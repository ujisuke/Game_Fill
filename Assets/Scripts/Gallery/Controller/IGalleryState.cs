namespace Assets.Scripts.Gallery.Controller
{
    public interface IGalleryState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
