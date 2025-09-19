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
        private bool isOnCooldownUp;
        private bool isOnCooldownDown;
        private bool isOnCooldownPause;
        private bool isPushingLeft;
        private bool isPushingRight;
        private bool isPushingUp;
        private bool isPushingDown;
        private readonly float cooldownSeconds = 0.3f;
        private static CustomInputSystem instance;
        public static CustomInputSystem Instance => instance ??= new CustomInputSystem();

        private CustomInputSystem()
        {
            isPushingSelect = false;
            isPushingBack = false;
            isOnCooldownLeft = false;
            isOnCooldownRight = false;
            isOnCooldownPause = false;
            isOnCooldownUp = false;
            isOnCooldownDown = false;
            isPushingLeft = false;
            isPushingRight = false;
            isPushingUp = false;
            isPushingDown = false;
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

        public bool GetPauseKey() => Input.GetKey(KeyCode.Escape);

        public bool GetLeftKeyDown()
        {
            if (isPushingLeft && GetLeftKey())
                return false;
            else if (isPushingLeft && !GetLeftKey())
            {
                isPushingLeft = false;
                return false;
            }
            else if (!isPushingLeft && GetLeftKey())
            {
                isPushingLeft = true;
                return true;
            }
            else
                return false;
        }

        public bool GetRightKeyDown()
        {
            if (isPushingRight && GetRightKey())
                return false;
            else if (isPushingRight && !GetRightKey())
            {
                isPushingRight = false;
                return false;
            }
            else if (!isPushingRight && GetRightKey())
            {
                isPushingRight = true;
                return true;
            }
            else
                return false;
        }

        public bool GetUpKeyDown()
        {
            if (isPushingUp && GetUpKey())
                return false;
            else if (isPushingUp && !GetUpKey())
            {
                isPushingUp = false;
                return false;
            }
            else if (!isPushingUp && GetUpKey())
            {
                isPushingUp = true;
                return true;
            }
            else
                return false;
        }

        public bool GetDownKeyDown()
        {
            if (isPushingDown && GetDownKey())
                return false;
            else if (isPushingDown && !GetDownKey())
            {
                isPushingDown = false;
                return false;
            }
            else if (!isPushingDown && GetDownKey())
            {
                isPushingDown = true;
                return true;
            }
            else
                return false;
        }
        
        private async UniTask CooldownLeft()
        {
            float cooldownSecondsDelta = cooldownSeconds * 0.1f;
            for (int i = 0; i < 10; i++)
            {
                if (!GetLeftKey())
                    break;
                await UniTask.Delay(TimeSpan.FromSeconds(cooldownSecondsDelta), ignoreTimeScale: true);
            }
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
            float cooldownSecondsDelta = cooldownSeconds * 0.1f;
            for (int i = 0; i < 10; i++)
            {
                if (!GetRightKey())
                    break;
                await UniTask.Delay(TimeSpan.FromSeconds(cooldownSecondsDelta), ignoreTimeScale: true);
            }
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

        private async UniTask CooldownUp()
        {
            float cooldownSecondsDelta = cooldownSeconds * 0.1f;
            for (int i = 0; i < 10; i++)
            {
                if (!GetUpKey())
                    break;
                await UniTask.Delay(TimeSpan.FromSeconds(cooldownSecondsDelta), ignoreTimeScale: true);
            }
            isOnCooldownUp = false;
        }

        public bool GetUpKeyWithCooldown()
        {
            if (isOnCooldownUp || !GetUpKey())
                return false;
            isOnCooldownUp = true;
            CooldownUp().Forget();
            return true;
        }

        private async UniTask CooldownDown()
        {
            float cooldownSecondsDelta = cooldownSeconds * 0.1f;
            for (int i = 0; i < 10; i++)
            {
                if (!GetDownKey())
                    break;
                await UniTask.Delay(TimeSpan.FromSeconds(cooldownSecondsDelta), ignoreTimeScale: true);
            }
            isOnCooldownDown = false;
        }

        public bool GetDownKeyWithCooldown()
        {
            if (isOnCooldownDown || !GetDownKey())
                return false;
            isOnCooldownDown = true;
            CooldownDown().Forget();
            return true;
        }

        public bool GetPauseKeyWithCooldown()
        {
            if (isOnCooldownPause)
            {
                if (!GetPauseKey())
                    isOnCooldownPause = false;
                return false;
            }
            if (!GetPauseKey())
                return false;
            isOnCooldownPause = true;
            return true;
        }
    }
}
