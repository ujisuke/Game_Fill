using System;
using System.Threading;
using Assets.Scripts.Player.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stage.View
{
    public class StageView : MonoBehaviour
    {
        [SerializeField] private ImageView backView;
        private CancellationTokenSource cTS;
        private CancellationToken token;

        private void Awake()
        {
            cTS = new();
            token = cTS.Token;
        }

        public void PlaySlowEffect()
        {
            if (PlayerModel.Instance == null)
                return;
            backView.transform.position = PlayerModel.Instance.Pos;
            backView.PlayAnim("SlowIn", 0.3f);
        }

        public void StopSlowEffect()
        {
            backView.PlayAnim("SlowOut");
        }

        public void OnDestroy()
        {
            cTS?.Cancel();
        }
    }
}
