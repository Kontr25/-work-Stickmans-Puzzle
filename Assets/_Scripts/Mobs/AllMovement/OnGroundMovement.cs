using _Scripts.GroundBlocks;
using _Scripts.Levels;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs.AllMovement
{
    public class OnGroundMovement : MoveBase
    {
        private MobStopPoint _startMoveStopPoint;
        public void SetValue(MobMover mobMover,Transform moverTransform,Rigidbody moverRigidbody, 
            float speed, int maximumNumberOfSteps, float stabilizeDuration)
        {
            Mover = mobMover;
            MoverTransform = moverTransform;
            MoverRigidbody = moverRigidbody;
            Speed = speed;
            MaximumNumberOfSteps = maximumNumberOfSteps;
            StabilizeDuration = stabilizeDuration;
            MobAnimator = Mover.Controller.MobAnimatorController;
        }

        public override void StartMove()
        {
            base.StartMove();
            _startMoveStopPoint = LastPoint;
            MoverRigidbody.isKinematic = false;
            IsMoving = true;
            if (MaximumNumberOfSteps > 0)
            {
                MobAnimator.Run();
            }
            else
            {
                MobAnimator.Walk();
            }
            
            IsCanMove = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (IsCanMove)
            {
                MoverRigidbody.velocity = MoverTransform.forward * Speed;
            }
        }

        public override void StopMove()
        {
            base.StopMove();
            if (CurrentActivatedBlock == null)
            {
                StepLimiter.Instance.Step();
            }
            StabilizePosition();
        }

        public override void IndentedStep()
        {
            base.IndentedStep();
            IsCanMove = false;
            MoverRigidbody.velocity = Vector3.zero;
            MoverRigidbody.isKinematic = true;
            //MobAnimator.Stumbling();
            MobAnimator.Idle();
            Vector3 lastStopPosition = new Vector3(LastPoint.transform.position.x, MoverTransform.position.y,
                LastPoint.transform.position.z);
            //MoverTransform.DOMove(lastStopPosition, 1f).onComplete = () =>
            MoverTransform.DOJump(lastStopPosition, .2f, 1, .2f).onComplete = () =>
            {
                IsMoving = false;
                if (LastPoint != _startMoveStopPoint && CurrentActivatedBlock == null)
                {
                    StepLimiter.Instance.Step();
                }
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

        public override void StabilizePosition()
        {
            base.StabilizePosition();
            if(IsDie) return;
            
            Mover.StopMove(); 
            
            Vector3 stabilizePosition = new Vector3(LastPoint.StabilisePoint.position.x,
                DefaultYPosition, LastPoint.StabilisePoint.position.z);
            MoverTransform.DOMove(stabilizePosition, StabilizeDuration).onComplete = () =>
            {
                IsMoving = false;
                MoverRigidbody.isKinematic = true;
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
                EndStabilizeAction();
            };
        }

        public override void TriggerEnter(Collider other)
        {
            base.TriggerEnter(other);
            if (other.TryGetComponent(out MobStopPoint stepPoint) && stepPoint != LastPoint && !stepPoint.IsFailPoint)
            {
                if(CurrentNumberOfSteps < MaximumNumberOfSteps && IsCanMove)
                {
                    LastPointMobStop = stepPoint;
                    CurrentNumberOfSteps++;
                }
                else
                {
                    LastPointMobStop = stepPoint;
                    if (_firstMove)
                    {
                        StopMove();
                    }
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

            if (other.TryGetComponent(out MobController mob) && IsCanMove || other.TryGetComponent(out Fence fence) && IsCanMove)
            {
                IndentedStep();
            }
        }

        public override void FailFall()
        {
            base.FailFall();
            MobAnimator.FailFallVertical();
            StopMove();
            MoverRigidbody.isKinematic = false;
            MoverRigidbody.useGravity = true;
            MoverRigidbody.AddForce(MoverTransform.forward * 4, ForceMode.Impulse);
        }
    }
}