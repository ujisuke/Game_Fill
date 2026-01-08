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
    public class PlayerStateMove : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;
        private bool isLookingLeft;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;
        private bool isStopping;

        public PlayerStateMove(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM, bool isLookingLeft = false, bool isInitial = false)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
            this.isLookingLeft = isLookingLeft;

            cTS = new CancellationTokenSource();
            token = cTS.Token;
            isStopping = false;

            if (!isInitial)
                return;
            if (CustomInputSystem.Instance.GetUpKey())
            {
                pM.MoveTurn(Vector2.up);
                pC.Compress(true);
            }
            else if (CustomInputSystem.Instance.GetDownKey())
            {
                pM.MoveTurn(Vector2.down);
                pC.Compress(true);
            }
            else if (CustomInputSystem.Instance.GetLeftKey())
            {
                pM.MoveTurn(Vector2.left);
                pC.Compress(true);
            }
            else if (CustomInputSystem.Instance.GetRightKey())
            {
                pM.MoveTurn(Vector2.right);
                pC.Compress(true);
            }
        }

        public void OnStateEnter()
        {
            pC.PlayAnim("Move");
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

            if (CustomInputSystem.Instance.GetSlowKeyDown())
            {
                AudioSourceView.Instance.PlaySlowSE();
                pM.AddSlowCount();
            }
            if (CustomInputSystem.Instance.GetSlowKey())
            {
                pM.Deceleration();
                pC.PlayAnim("MoveSlow");
                AudioSourceView.Instance.CutOffBGM();
            }
            else
            {
                pM.Acceleration();
                pC.PlayAnim("Move");
                AudioSourceView.Instance.RestoreBGM();
            }

            if (StageModel.Instance.IsPlayerHittingWall(pM.HurtBox) || !StageModel.Instance.IsPlayerOnBlock(pM.Pos) || StageModel.Instance.TimeLimit <= 0)
                pSM.ChangeState(new PlayerStateDead(pM, pC, pSM));
            else if (pM.IsOnExit)
                pSM.ChangeState(new PlayerStateExit(pM, pC, pSM));
            else if (CustomInputSystem.Instance.GetFillKey() && !isStopping)
                pSM.ChangeState(new PlayerStateFill(pM, pC, pSM, isLookingLeft));
        }

        public void OnStateExit()
        {
            cTS.Cancel();
            cTS.Dispose();
        }

        private async UniTask StopAndGo()  //方向転換の瞬間に少しだけ動きを止める
        {
            isStopping = true;
            await UniTask.Delay(TimeSpan.FromSeconds(pC.StopSeconds), cancellationToken: token);
            isStopping = false;
        }
    }
}