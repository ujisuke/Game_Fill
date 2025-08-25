using Assets.Scripts.Map.Data;
using Assets.Scripts.Player.Controller;
using UnityEngine;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateMachine : MonoBehaviour
    {
        [SerializeField] private bool isFinalStage;
        [SerializeField] private string nextStageName;
        [SerializeField] private SceneNameData sceneNameData;
        private IStageState currentState;
        public bool IsFinalStage => isFinalStage;
        public string NextStageName => nextStageName;
        public string MapSceneName => sceneNameData.MapSceneName;

        private void Awake()
        {
            currentState = new StageStateInitial(this);
            currentState.OnStateEnter();
        }

        public void ChangeState(IStageState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }

        public void Update()
        {
            currentState.HandleInput();
        }
    }
}
