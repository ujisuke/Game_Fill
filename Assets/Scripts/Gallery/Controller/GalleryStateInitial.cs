namespace Assets.Scripts.Gallery.Controller
{
    public class GalleryStateInitial : IGalleryState
    {
        private readonly GalleryStateMachine gSM;
        private readonly GalleryController gC;
        private static bool isFromTitle;

        public GalleryStateInitial(GalleryStateMachine stateMachine, GalleryController gC)
        {
            gSM = stateMachine;
            this.gC = gC;
        }

        public void OnStateEnter()
        {
            gC.Initialize();
            if (isFromTitle)
                gC.OpenSceneFromTitle();
            else
                gC.OpenSceneNotFromTitle();
            isFromTitle = false;
        }

        public void HandleInput()
        {
            gSM.ChangeState(new GalleryStateSelectStage(gSM, gC));
        }

        public void OnStateExit()
        {

        }

        public static void OpenFromTitle()
        {
            isFromTitle = true;
        }
    }
}
