using _Scripts.GroundBlocks;
using _Scripts.Levels;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs.AllMovement
{
    public class JumpOneStepMovement : MoveBase
    {
        private float _jumpForce;
        private float _moveDistance;
        private float _jumpDuration;
        private float _elapsedTime;
        private Vector3 _startPosition;
        private Vector3 _endPosition;

        public void SetValue(MobMover mobMover, Transform moverTransform,Rigidbody moverRigidbody, 
            float stabilizeDuration, float jumpForce, float moveDistance, float jumpDuration)
        {
            Mover = mobMover;
            MoverTransform = moverTransform;
            MoverRigidbody = moverRigidbody;
            StabilizeDuration = stabilizeDuration;
            _jumpForce = jumpForce;
            _moveDistance = moveDistance;
            _jumpDuration = jumpDuration;
            MobAnimator = Mover.Controller.MobAnimatorController;
        }

        public override void StartMove()
        {
            base.StartMove();
            IsMoving = true;
            MobAnimator.Jump();
            MoverRigidbody.isKinematic = true;
            _elapsedTime = 0f;
            _startPosition = MoverTransform.position;
            _endPosition = _startPosition + MoverTransform.forward * _moveDistance;
            DeferredJump();
        }


        public override void FixedUpdate()
        {
            if (DeferredStarted)
            {
                if (DeferredElapsedTime < DeferredDuration)
                {
                    DeferredElapsedTime += Time.fixedDeltaTime;
                }
                else
                {
                    IsCanMove = true;
                    DeferredStarted = false;
                    DeferredElapsedTime = 0;
                }
            }
            
            if (IsCanMove) {
                if (_elapsedTime < _jumpDuration)
                {
                    JumpMove();
                }
                else
                {
                    MobAnimator.Landing();
                    IsCanMove = false;
                    LastTriggeredMob = null;
                    if (CurrentActivatedBlock == null)
                    {
                        StepLimiter.Instance.Step();
                    }
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
        
        public override void IndentedStep()
        {
            base.IndentedStep();
            IsCanMove = false;
            MobAnimator.IndentedJump();
            LastTriggeredMob.MobAnimatorController.Hit();
            _elapsedTime = 0f;
            _startPosition = MoverTransform.position;
            _endPosition = new Vector3(LastTriggeredMob.transform.position.x, DefaultYPosition, LastTriggeredMob.transform.position.z) 
                           + MoverTransform.forward * _moveDistance;
            DeferredJump();
        }

        public override void TriggerEnter(Collider other)
        {
            base.TriggerEnter(other);
            if (other.TryGetComponent(out MobStopPoint stepPoint) && stepPoint != LastPoint && !stepPoint.IsFailPoint)
            {
                if (CurrentNumberOfSteps < MaximumNumberOfSteps && IsCanMove)
                {
                    LastPointMobStop = stepPoint;
                    CurrentNumberOfSteps++;
                }
                else
                {
                    LastPointMobStop = stepPoint;
                }
            } 
            else if (other.TryGetComponent(out MobStopPoint stopPoint) && stopPoint != LastPoint && stopPoint.IsFailPoint)
            {
                LastPointMobStop = stopPoint;
                if (_firstMove && !IsDie)
                {
                    FailFall();
                }
            }
            
            if (other.TryGetComponent(out MobController mob) && IsCanMove && mob != LastTriggeredMob)
            {
                LastTriggeredMob = mob;
                IndentedStep();
            }
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
            IsCanMove = false;
        }
        
        public override void FailFall()
        {
            base.FailFall();
            StopMove();
            MobAnimator.Fall();
            MoverRigidbody.isKinematic = false;
            MoverRigidbody.useGravity = true;
            MoverRigidbody.AddForce(MoverTransform.forward * 2 - MoverTransform.up * 4, ForceMode.Impulse);
        }
    }
}