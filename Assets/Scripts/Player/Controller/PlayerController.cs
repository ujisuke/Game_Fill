using System.Threading;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerView playerView;
        private PlayerModel playerModel;
        private PlayerStateMachine pSM;
        private CancellationToken token;
        public CancellationToken Token => token;
        public float StopSeconds => playerData.StopSeconds;

        private void Awake()
        {
            playerModel = new PlayerModel(playerData, transform.position);
            playerView.SetPos(playerModel.Pos);
            playerView.InstantiateHurtBox(playerModel.HurtBox);
            pSM = new PlayerStateMachine(playerModel, this);
            token = this.GetCancellationTokenOnDestroy();
        }

        private void Update()
        {
            pSM.HandleInput();
            playerView.SetPos(playerModel.Pos);
            playerView.SetHurtBoxPos(playerModel.HurtBox);
        }

        public void PlayAnim(string animName, float animSeconds = 0f)
        {
            playerView.PlayAnim(animName, animSeconds);
        }

        public void StopAnim()
        {
            playerView.StopAnim();
        }

        public void FlipX(bool isLeft)
        {
            playerView.FlipX(isLeft);
        }

        public void Compress(bool isCompress)
        {
            playerView.Compress(isCompress);
        }

        public void OnDestroy()
        {
            playerView.DestroyHurtBox();
            Destroy(gameObject);
        }
    }
}
