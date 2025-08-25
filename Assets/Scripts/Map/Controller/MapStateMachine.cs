using Assets.Scripts.Map.Data;
using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateMachine : MonoBehaviour
    {
        [SerializeField] private SceneNameData stageNameData;
        private IMapState currentState;
        public SceneNameData StageNameData => stageNameData;

        private void Awake()
        {
            currentState = new MapStateInitial(this);
            currentState.OnStateEnter();
        }

        public void ChangeState(IMapState newState)
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
