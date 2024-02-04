using System;
using _Scripts.Mobs.AllMovement;
using _Scripts.Mobs.MovementConfigs;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private MobController _mobController;
        [SerializeField] private Collider[] _colliders;

        [SerializeField] private MoveBase _mover;

        public MobController Controller
        {
            get => _mobController;
            set => _mobController = value;
        }

        public MoveBase Mover
        {
            get => _mover;
            set => _mover = value;
        }

        public void SetMover(MobType mobType)
        {
            if (Mover != null)
            {
                Destroy(Mover);
            }
            switch (mobType)
            {
                case MobType.OneStepMovement:
                    OnGroundMovement OneStep = gameObject.AddComponent<OnGroundMovement>();
                    OneStep.SetValue(this, transform, _rigidbody, _movementConfig.Speed, 
                        _movementConfig.MaxStepsForOneStepMover, _movementConfig.WalkingStabilizeDuration);
                    Mover = OneStep;
                    break;
                
                case MobType.DoubleStepMovement:
                    OnGroundMovement doubleStep = gameObject.AddComponent<OnGroundMovement>();
                    doubleStep.SetValue(this, transform, _rigidbody, _movementConfig.Speed, 
                        _movementConfig.MaxStepsForDoubleStepMover, _movementConfig.WalkingStabilizeDuration);
                    Mover = doubleStep;
                    break;
                
                case MobType.JumpOneStepMovement:
                    JumpOneStepMovement jumper = gameObject.AddComponent<JumpOneStepMovement>();
                    jumper.SetValue(this, transform, _rigidbody, _movementConfig.JumpStabilize,
                        _movementConfig.JumpForce, _movementConfig.JumpDistance, _movementConfig.JumpDuration);
                    Mover = jumper;
                    break;
                
                case MobType.UnlimitedStepMovement:
                    OnGroundMovement unlimitedStep = gameObject.AddComponent<OnGroundMovement>();
                    unlimitedStep.SetValue(this, transform, _rigidbody, _movementConfig.Speed, 
                        _movementConfig.MaxStepsForUnlimitedMover, _movementConfig.WalkingStabilizeDuration);
                    Mover = unlimitedStep;
                    break;
                
                case MobType.GlideTwoStepMovement:
                    GlideTwoStepMovement flyer = gameObject.AddComponent<GlideTwoStepMovement>();
                    flyer.SetValue(this, transform, _rigidbody, _movementConfig.Speed, _movementConfig.MaxStepsForGlideMover,
                        _movementConfig.RiseDuration, _movementConfig.RiseDuration, _movementConfig.RiseHeight,
                        _movementConfig.JumpForce, _movementConfig.JumpDistance, _movementConfig.JumpDuration);
                    Mover = flyer;
                    break;
            }
            
            Mover.Start();
            UpdateTrigger();
        }

        private void FixedUpdate()
        {
            Mover.FixedUpdate();
        }

        public void StartMove()
        {
            Mover.StartMove();
        }

        public void StopMove()
        {
            Controller.StopMove();
        }

        private void OnTriggerEnter(Collider other)
        {
            Mover.TriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Mover.TriggerExit(other);
        }

        public void UpdateTrigger()
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = false;
                _colliders[i].enabled = true;
            }
        }
    }
}