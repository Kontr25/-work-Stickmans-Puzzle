using System.Collections;
using _Scripts.GroundBlocks;
using _Scripts.Levels;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs.AllMovement
{
    public class GlideTwoStepMovement : MoveBase
    {
        private float _riseDuration;
        private float _riseHeight;
        private float _jumpForce;
        private float _moveDistance;
        private float _jumpDuration;
        private float _elapsedTime;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private bool _jump = false;
        private bool _inIndentedJump = false;
        
        public void SetValue(MobMover mobMover, Transform moverTransform,Rigidbody moverRigidbody, 
            float speed, int maximumNumberOfSteps, float stabilizeDuration, float riseDuration, float riseHeight,
            float jumpForce, float moveDistance, float jumpDuration)
        {
            Mover = mobMover;
            MoverTransform = moverTransform;
            MoverRigidbody = moverRigidbody;
            Speed = speed;
            MaximumNumberOfSteps = maximumNumberOfSteps;
            StabilizeDuration = stabilizeDuration;
            _riseDuration = riseDuration;
            _riseHeight = riseHeight;
            _jumpForce = jumpForce;
            _moveDistance = moveDistance;
            _jumpDuration = jumpDuration;
            MobAnimator = Mover.Controller.MobAnimatorController;
        }

        public override void StartMove()
        {
            base.StartMove();
            CurrentNumberOfSteps = 0;
            IsMoving = true;
            MobAnimator.Rise();
            MoverRigidbody.useGravity = false;
            MoverRigidbody.isKinematic = true;
            Vector3 _risePosition = new Vector3(MoverTransform.position.x, MoverTransform.position.y + _riseHeight,
                MoverTransform.position.z);
            
            MoverTransform.DOMove(_risePosition, _riseDuration).onComplete = () =>
            {
                MoverRigidbody.isKinematic = false;
                IsCanMove = true;
            };
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (DeferredStarted)
            {
                if (DeferredElapsedTime < DeferredDuration)
                {
                    DeferredElapsedTime += Time.fixedDeltaTime;
                }
                else
                {
                    _jump = true;
                    DeferredStarted = false;
                    DeferredElapsedTime = 0;
                }
            }
            
            if (IsCanMove)
            {
                MoverRigidbody.velocity = MoverTransform.forward * Speed;
            }
            
            if (_jump) {
                if (_elapsedTime < _jumpDuration)
                {
                    JumpMove();
                }
                else
                {
                    _jump = false;
                    StabilizePosition();
                }
            }
        }
        
        private void JumpMove()
        {
            float y = _jumpForce * (1f - (_elapsedTime / _jumpDuration)) * (_elapsedTime / _jumpDuration);
            MoverRigidbody.MovePosition(Vector3.Lerp(_startPosition, _endPosition, _elapsedTime / _jumpDuration) + new Vector3(0f, y, 0f));
            _elapsedTime += Time.fixedDeltaTime;
        }

        public override void StopMove()
        {
            base.StopMove();
            MoverRigidbody.isKinematic = false;
            MoverRigidbody.useGravity = true;
            MobAnimator.Fall();
        }
        
        public override void IndentedStep()
        {
            base.IndentedStep();
            _inIndentedJump = true;
            DeferredStarted = false;
            MoverRigidbody.isKinematic = true;
            MobAnimator.IndentedJump();
            LastTriggeredMob.MobAnimatorController.Hit();
            _jump = false;
            _elapsedTime = 0f;
            _startPosition = MoverTransform.position;
            _endPosition = new Vector3(LastTriggeredMob.transform.position.x, DefaultYPosition, LastTriggeredMob.transform.position.z) 
                           + MoverTransform.forward * _moveDistance;
            DeferredJump();
        }

        public override void TriggerEnter(Collider other)
        {
            base.TriggerEnter(other);
            if (other.TryGetComponent(out MobStopPoint stepPoint) && stepPoint != LastPoint)
            {
                if (CurrentNumberOfSteps < MaximumNumberOfSteps && IsCanMove)
                {
                    LastPointMobStop = stepPoint;
                    CurrentNumberOfSteps++;
                }
                else
                {
                    LastPointMobStop = stepPoint;
                    if (_firstMove && !_inIndentedJump)
                    {
                        if (!stepPoint.IsFailPoint)
                        {
                            StopMove();
                        }
                        else
                        {
                            FailFall();
                        }
                    }
                }
            }

            if (other.TryGetComponent(out MobController mob) && IsMovingNow && mob != LastTriggeredMob)
            {
                LastTriggeredMob = mob;
                IndentedStep();
            }

            if (other.TryGetComponent(out StabilizePoint stabilizePoint) && lastStabilizePoint != stabilizePoint)
            {
                if (_firstMove && !IsDie)
                {
                    lastStabilizePoint = stabilizePoint;
                    Landing();
                }
            }
        }

        private void Landing()
        {
            if(IsDie) return;
            if (CurrentActivatedBlock == null)
            {
                StepLimiter.Instance.Step();
            }
            StabilizePosition();
            MobAnimator.Landing();
            IsCanMove = false;
            MoverRigidbody.isKinematic = true;
        }

        public override void StabilizePosition()
        {
            base.StabilizePosition();
            if(LastPoint != null)
            {
                Vector3 stabilizePosition = new Vector3(LastPoint.StabilisePoint.position.x,
                DefaultYPosition, LastPoint.StabilisePoint.position.z);
                MoverTransform.DOMove(stabilizePosition, StabilizeDuration).onComplete = () =>
                {
                    IsMoving = false;
                    _inIndentedJump = false;
                    if (CurrentActivatedBlock != null)
                    {
                        CurrentActivatedBlock.Activate(Mover.Controller);
                    }

                    if (_blockSkinChanger != null)
                    {
                        Mover.Controller.WithoutSkin = false;
                        Mover.Controller.SetSkin(_blockSkinChanger.ChangeMobType);
                        _blockSkinChanger.Change();
                    }
                };
            }
            LastTriggeredMob = null;
            IsCanMove = false;
        }


        public override void FailFall()
        {
            base.FailFall();
            StopMove();
            _jump = false;
            //MoverRigidbody.AddForce(MoverTransform.forward * 4, ForceMode.Impulse);
        }
    }
}