using UnityEngine;

namespace _Scripts.Mobs.MovementConfigs
{
    [CreateAssetMenu (fileName = nameof(MovementConfig), menuName = "Configs/" + nameof(MovementConfig))]
    public class MovementConfig: ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _walkingStabilizeDuration;
        
        [Header("DoubleStepMovementConfiguration")] 
        [SerializeField] private int _maxStepsForDoubleStepMover;

        [Header("OneStepMovementConfiguration")] 
        [SerializeField] private int _maxStepsForOneStepMover;
        
        [Header("JumpConfiguration")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _jumpStabilize;
        
        [Header("GlideConfiguration")]
        [SerializeField] private int _maxStepsForGlideMover;
        [SerializeField] private float _riseHeight;
        [SerializeField] private float _riseDuration;
        
        [Header("UnlimitedConfiguration")]
        [SerializeField] private int _maxStepsForUnlimitedMover;

        public int MaxStepsForDoubleStepMover => _maxStepsForDoubleStepMover;

        public int MaxStepsForOneStepMover => _maxStepsForOneStepMover;

        public float JumpForce => _jumpForce;

        public float JumpDuration => _jumpDuration;

        public float RiseHeight => _riseHeight;

        public float RiseDuration => _riseDuration;

        public int MaxStepsForUnlimitedMover => _maxStepsForUnlimitedMover;

        public float Speed => _speed;

        public int MaxStepsForGlideMover => _maxStepsForGlideMover;

        public float JumpDistance => _jumpDistance;

        public float JumpStabilize => _jumpStabilize;

        public float WalkingStabilizeDuration => _walkingStabilizeDuration;
    }
}