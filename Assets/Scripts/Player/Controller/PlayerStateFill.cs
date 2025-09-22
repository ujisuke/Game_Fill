using System;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateFill : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;
        private bool isLookingLeft;
        private CancellationTokenSource cTS;
        private CancellationToken token;
        private bool isStopping;

        public PlayerStateFill(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM, bool isLookingLeft = false)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
            this.isLookingLeft = isLookingLeft;
            cTS = new CancellationTokenSource();
            token = cTS.Token;
            isStopping = false;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.PlayFillSE();
            pC.PlayAnim("Fill");
        }

        public void HandleInput()
        {
            if (!isStopping)
            {
                if (CustomInputSystem.Instance.GetUpKeyDown())
                {
                    AudioSourceView.Instance.PlayTurnSE();
                    pM.MoveTurn(Vector2.up);
                    pC.Compress(true);
                    StopAndGo().Forget();
                }
                else if (CustomInputSystem.Instance.GetDownKeyDown())
                {
                    AudioSourceView.Instance.PlayTurnSE();
                    pM.MoveTurn(Vector2.down);
                    pC.Compress(true);
                    StopAndGo().Forget();
                }
                else if (CustomInputSystem.Instance.GetLeftKeyDown())
                {
                    AudioSourceView.Instance.PlayTurnSE();
                    pM.MoveTurn(Vector2.left);
                    pC.Compress(true);
                    StopAndGo().Forget();
                }
                else if (CustomInputSystem.Instance.GetRightKeyDown())
                {
                    AudioSourceView.Instance.PlayTurnSE();
                    pM.MoveTurn(Vector2.right);
                    pC.Compress(true);
                    StopAndGo().Forget();
                }
                else
                {
                    pM.MoveStraight();
                    pC.Compress(false);
                }
            }
            isLookingLeft = CustomInputSystem.Instance.GetLeftKey() || (isLookingLeft && !CustomInputSystem.Instance.GetRightKey());
            pC.FlipX(isLookingLeft);

            StageModel.Instance.FillBlock(pM.HurtBox);

            if (CustomInputSystem.Instance.GetSlowKeyDown())
                AudioSourceView.Instance.PlaySlowSE();
            if (CustomInputSystem.Instance.GetSlowKey())
            {
                pM.Deceleration();
                pC.PlayAnim("FillSlow");
                AudioSourceView.Instance.CutOffBGM();
            }
            else
            {
                pM.Acceleration();
                pC.PlayAnim("Fill");
                AudioSourceView.Instance.RestoreBGM();
            }
            
            if (StageModel.Instance.IsPlayerHittingWall(pM.HurtBox) || !StageModel.Instance.IsPlayerOnBlock(pM.Pos) || StageModel.Instance.TimeLimit <= 0)
                pSM.ChangeState(new PlayerStateDead(pM, pC, pSM));
            else if (pM.IsOnExit)
                pSM.ChangeState(new PlayerStateExit(pM, pC, pSM));
            else if (!Input.GetMouseButton(0) && !isStopping)
                pSM.ChangeState(new PlayerStateMove(pM, pC, pSM, isLookingLeft));
        }

        private async UniTask StopAndGo()
        {
            isStopping = true;
            await UniTask.Delay(TimeSpan.FromSeconds(pC.StopSeconds), cancellationToken: token);
            isStopping = false;
        }

        public void OnStateExit()
        {
            AudioSourceView.Instance.FadeOutFillSE().Forget();
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}