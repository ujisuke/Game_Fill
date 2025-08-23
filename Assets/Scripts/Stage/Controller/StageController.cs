using Assets.Scripts.Stage.Model;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Stage.Controller
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        private StageModel stageModel;

        private void Awake()
        {
            stageModel = new StageModel(tilemap);
        }
    }
}
