using System;
using System.Collections;
using _Scripts.Mobs.AllAnimators;
using _Scripts.Mobs.MovementConfigs;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _hitDuration;

        private Coroutine _hitRoutine;
        private WaitForSeconds _hitDelay;
        
        private static readonly int WalkTrigger = Animator.StringToHash("Walk");
        private static readonly int RunTrigger = Animator.StringToHash("Run");
        private static readonly int RiseTrigger = Animator.StringToHash("Rise");
        private static readonly int FallTrigger = Animator.StringToHash("Fall");
        private static readonly int IdleTrigger = Animator.StringToHash("Idle");
        private static readonly int LandingTrigger = Animator.StringToHash("Landing");
        private static readonly int JumpTrigger = Animator.StringToHash("Jump");
        private static readonly int StumblingTrigger = Animator.StringToHash("Stumbling");
        private static readonly int FailTrigger = Animator.StringToHash("Fail");
        private static readonly int IndentedJumpTrigger = Animator.StringToHash("IndentedJump");
        private static readonly int RightTurnTrigger = Animator.StringToHash("RightTurn");
        private static readonly int LeftTurnTrigger = Animator.StringToHash("LeftTurn");
        private static readonly int HitTrigger = Animator.StringToHash("Hit");

        private void Start()
        {
            _hitDelay = new WaitForSeconds(_hitDuration);
        }

        public void DisableAnimator()
        {
            _animator.enabled = false;
            enabled = false;
        }

        public void Idle()
        {
            ResetAllTrigger();
            _animator.SetTrigger(IdleTrigger);
            print(nameof(Idle));
        }

        public void Walk()
        {
            ResetAllTrigger();
            _animator.SetTrigger(WalkTrigger);
        }

        public void Run()
        {
            ResetAllTrigger();
            _animator.SetTrigger(RunTrigger);
        }

        public void Rise()
        {
            ResetAllTrigger();
            _animator.SetTrigger(RiseTrigger);
        }

        public void Fall()
        {
            ResetAllTrigger();
            _animator.SetTrigger(FallTrigger);
        }
        
        public void Landing()
        {
            ResetAllTrigger();
            _animator.SetTrigger(LandingTrigger);
        }
        public void Jump()
        {
            ResetAllTrigger();
            _animator.SetTrigger(JumpTrigger);
        }

        public void Stumbling()
        {
            ResetAllTrigger();
            _animator.SetTrigger(StumblingTrigger);
        }

        public void FailFallVertical()
        {
            ResetAllTrigger();
            _animator.SetTrigger(FailTrigger);
        }

        public void IndentedJump()
        {
            ResetAllTrigger();
            _animator.SetTrigger(IndentedJumpTrigger);
        }
        
        public void LeftTurn()
        {
            ResetAllTrigger();
            _animator.SetTrigger(LeftTurnTrigger);
        }
        
        public void RightTurn()
        {
            ResetAllTrigger();
            _animator.SetTrigger(RightTurnTrigger);
        }

        public void Hit()
        {
            ResetAllTrigger();
            if (_hitRoutine != null)
            {
                StopCoroutine(_hitRoutine);
                _hitRoutine = null;
            }

            _hitRoutine = StartCoroutine(HitRoutine());
        }

        private IEnumerator HitRoutine()
        {
            _animator.SetLayerWeight(1, 1);
            _animator.SetTrigger(HitTrigger);
            yield return _hitDelay;
            _animator.SetLayerWeight(1, 0);
        }

        private void ResetAllTrigger()
        {
            _animator.ResetTrigger(IdleTrigger);
            _animator.ResetTrigger(WalkTrigger);
            _animator.ResetTrigger(RunTrigger);
            _animator.ResetTrigger(FallTrigger);
            _animator.ResetTrigger(LandingTrigger);
            _animator.ResetTrigger(JumpTrigger);
            _animator.ResetTrigger(FailTrigger);
            _animator.ResetTrigger(IndentedJumpTrigger);
            _animator.ResetTrigger(RiseTrigger);
            _animator.ResetTrigger(LeftTurnTrigger);
            _animator.ResetTrigger(RightTurnTrigger);
            _animator.ResetTrigger(HitTrigger);
            if (_animator.GetLayerWeight(1) > 0)
            {
                _animator.SetLayerWeight(1, 0);
            }
        }
    }
}