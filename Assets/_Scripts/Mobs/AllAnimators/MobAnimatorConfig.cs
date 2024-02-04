using UnityEngine;

namespace _Scripts.Mobs.AllAnimators
{
    [CreateAssetMenu (fileName = nameof(MobAnimatorConfig), menuName = "Configs/" + nameof(MobAnimatorConfig))]
    public class MobAnimatorConfig: ScriptableObject
    {
        private readonly int _idleTrigger = Animator.StringToHash("Idle");
        private readonly int _walkTrigger = Animator.StringToHash("Walk");
        private readonly int _runTrigger = Animator.StringToHash("Run");
        private readonly int _jumpTrigger = Animator.StringToHash("Jump");
        private readonly int _landingTrigger = Animator.StringToHash("Landing");
        private readonly int _riseTrigger = Animator.StringToHash("Rise");
        private readonly int _flyTrigger = Animator.StringToHash("Fly");
        private readonly int _fallTrigger = Animator.StringToHash("Fall");

        public int IdleTrigger => _idleTrigger;

        public int WalkTrigger => _walkTrigger;

        public int RunTrigger => _runTrigger;

        public int JumpTrigger => _jumpTrigger;

        public int LandingTrigger => _landingTrigger;

        public int RiseTrigger => _riseTrigger;

        public int FlyTrigger => _flyTrigger;

        public int FallTrigger => _fallTrigger;
    }
}