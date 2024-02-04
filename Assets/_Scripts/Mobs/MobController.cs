using System;
using _Scripts.Creative;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobController : MonoBehaviour
    {
        [SerializeField] private bool _withoutSkin;
        [SerializeField] private MobRotator _mobRotator;
        [SerializeField] private MobMover _mobMover;
        [SerializeField] private MobAnimator _mobAnimator;
        [SerializeField] private MobType mobType;
        [SerializeField] private MobDisappearer _mobDisappearer;
        [SerializeField] private SelectionView _selectionView;
        [SerializeField] private MobSkinController _mobSkinController;
        [SerializeField] private MobSounds _mobSounds;
        [SerializeField] private MobRagdoll _mobRagdoll;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isDie = false;

        public MobAnimator MobAnimatorController
        {
            get => _mobAnimator;
            set => _mobAnimator = value;
        }

        public MobMover Mover
        {
            get => _mobMover;
            set => _mobMover = value;
        }

        public bool WithoutSkin
        {
            get => _withoutSkin;
            set => _withoutSkin = value;
        }

        public void Rotate(RotatingSide _rotatingSide)
        {
            _mobRotator.RotateMob(_rotatingSide);
        }

        private void Awake()
        {
            SetSkin(mobType);
        }

        public void SetSkin(MobType mobType)
        {
            Mover.SetMover(mobType);
            if (!WithoutSkin)
            {
                _mobSkinController.SetSkin(mobType);
            }
            else
            {
                _mobSkinController.SetSkin(MobType.None);
            }
        }

        public void Move()
        {
            Mover.StartMove();
        }

        public void StopMove()
        {
            MobAnimatorController.Idle();
        }

        public void Disappear(Transform block)
        {
            transform.SetParent(block);
            _mobDisappearer.Disappear();
        }

        public void StepBack()
        {
            throw new NotImplementedException();
        }

        public void Selected()
        {
            _selectionView.Selected();
        }
        
        public void DiSelected()
        {
            _selectionView.DiSelected();
        }

        public void Fail()
        {
            _mobSounds.Screech();
        }

        public void DieOnTrap(Vector3 explosionPosition, float explosionForce, float explosionRadius)
        {
            if(_isDie) return;
            _isDie = true;
            _rigidbody.isKinematic = true;
            _mobMover.enabled = false;
            _mobAnimator.DisableAnimator();
            _mobRagdoll.EnableRagdoll(explosionPosition, explosionForce, explosionRadius);
        }
    }
}