using _Scripts.Creative;
using _Scripts.GroundBlocks;
using _Scripts.Levels;
using UnityEngine;

namespace _Scripts.Mobs.AllMovement
{
    public abstract class MoveBase: MonoBehaviour
    {
        protected float StabilizeDuration;
        protected MobMover Mover;
        protected Transform MoverTransform;
        protected Rigidbody MoverRigidbody;
        [SerializeField] protected MobStopPoint LastPointMobStop;
        protected float Speed;
        protected bool IsCanMove;
        protected int MaximumNumberOfSteps;
        protected int CurrentNumberOfSteps;
        protected float DefaultYPosition;
        protected bool _firstMove = false;
        protected StabilizePoint lastStabilizePoint;
        protected MobAnimator MobAnimator;
        protected bool IsDie = false;
        protected ActivatedBlockSide CurrentActivatedBlock;
        protected bool DeferredStarted = false;
        protected float DeferredDuration = 0.2f;
        protected float DeferredElapsedTime = 0f;
        protected MobController LastTriggeredMob;
        protected bool IsMoving = false;
        protected BlockSkinChanger _blockSkinChanger;

        public MobStopPoint LastPoint => LastPointMobStop;

        public bool IsMovingNow => IsMoving;


        public void Start()
        {
            DefaultYPosition = MoverTransform.position.y;
        }
        
        public virtual void FixedUpdate()
        {
            
        }
        
        protected void DeferredJump()
        {
            DeferredElapsedTime = 0;
            DeferredStarted = true;
        }
        
        public virtual void StartMove()
        {
            _firstMove = true;
            CurrentNumberOfSteps = 0;
        }

        public virtual void IndentedStep()
        {
            
        }

        public virtual void StopMove()
        {
            IsCanMove = false;
            MoverRigidbody.velocity = Vector3.zero;
        }

        public virtual void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ActivatedBlockSide activatedBlockSide))
            {
                CurrentActivatedBlock = activatedBlockSide;
            }
            if (other.TryGetComponent(out BlockSkinChanger skinChanger))
            {
                _blockSkinChanger = skinChanger;
            }
        }
        
        public virtual void TriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ActivatedBlockSide activatedBlockSide) && activatedBlockSide == CurrentActivatedBlock)
            {
                CurrentActivatedBlock = null;
            }
            
            if (other.TryGetComponent(out BlockSkinChanger skinChanger))
            {
                _blockSkinChanger = null;
            }
        }

        public virtual void StabilizePosition()
        {
        }

        public virtual void EndStabilizeAction()
        {
            
        }

        public void MoveCompleted()
        {
            
        }

        public virtual void FailFall()
        {
            IsDie = true;
            Mover.Controller.Fail();
        }
    }
}