using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Common.Controller
{
    public class CustomInputSystem
    {
        private bool isPushingSelect;
        private bool isPushingBack;
        private bool isOnCooldownLeft;
        private bool isOnCooldownRight;
        private readonly float cooldownSeconds = 0.3f;
        private static CustomInputSystem instance;
        public static CustomInputSystem Instance => instance ??= new CustomInputSystem();

        private CustomInputSystem()
        {
            isPushingSelect = false;
            isPushingBack = false;
            isOnCooldownLeft = false;
            isOnCooldownRight = false;
        }

        public bool DoesSelectKeyUp()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isPushingSelect = true;
                return false;
            }
            if (isPushingSelect)
            {
                isPushingSelect = false;
                return true;
            }
            return false;
        }

        public bool DoesBackKeyUp()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                isPushingBack = true;
                return false;
            }
            if (isPushingBack)
            {
                isPushingBack = false;
                return true;
            }
            return false;
        }

        public bool GetLeftKey() => Input.GetKey(KeyCode.A);

        public bool GetRightKey() => Input.GetKey(KeyCode.D);

        public bool GetUpKey() => Input.GetKey(KeyCode.W);

        public bool GetDownKey() => Input.GetKey(KeyCode.S);

        private async UniTask CooldownLeft()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(cooldownSeconds));
            isOnCooldownLeft = false;
        }

        public bool GetLeftKeyWithCooldown()
        {
            if (isOnCooldownLeft || !GetLeftKey())
                return false;
            isOnCooldownLeft = true;
            CooldownLeft().Forget();
            return true;
        }

        private async UniTask CooldownRight()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(cooldownSeconds));
            isOnCooldownRight = false;
        }

        public bool GetRightKeyWithCooldown()
        {
            if (isOnCooldownRight || !GetRightKey())
                return false;
            isOnCooldownRight = true;
            CooldownRight().Forget();
            return true;
        }
    }
}
