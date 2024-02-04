using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class BlockAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int Activated = Animator.StringToHash("Activated");
        private static readonly int NotActivated = Animator.StringToHash("NotActivated");
        private static readonly int RotateTrigger = Animator.StringToHash("Rotate");

        public void ActivatedRotation()
        {
            _animator.SetTrigger(Activated);
        }

        public void NotAvtivatedRotation()
        {
            _animator.SetTrigger(NotActivated);
        }

        public void Rotate()
        {
            _animator.SetTrigger(RotateTrigger);
        }
    }
}