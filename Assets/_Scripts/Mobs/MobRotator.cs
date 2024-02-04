using System;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobRotator : MonoBehaviour
    {
        [SerializeField] private float _rotateDuration;
        [SerializeField] private MobController _mobController;

        private RotatingSide _previousSide = RotatingSide.None;
        private Quaternion _targetRotation;
        
        public void RotateMob(RotatingSide side)
        {
            print(side);
            switch (side)
            {
                case RotatingSide.Right:
                    _targetRotation = Quaternion.Euler(0, 90, 0);
                    break;
                case RotatingSide.Left:
                    _targetRotation = Quaternion.Euler(0, 270, 0);
                    break;
                case RotatingSide.Forward:
                    _targetRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case RotatingSide.Backward:
                    _targetRotation = Quaternion.Euler(0, 180, 0);
                    break;
            }
            
            Rotate(side);
        }

        private void Rotate(RotatingSide side)
        {
            if (Math.Abs(transform.eulerAngles.y - _targetRotation.eulerAngles.y) > 0.1f)
            {
                transform.DORotate(new Vector3(0, _targetRotation.eulerAngles.y, 0), _rotateDuration).onComplete = () =>
                {
                    _mobController.Move();
                };
                if (side == RotatingSide.Right || side == RotatingSide.Forward)
                {
                    _mobController.MobAnimatorController.LeftTurn();
                }
                else
                {
                    _mobController.MobAnimatorController.RightTurn();
                }
            }
            else
            {
                _mobController.Move();
            }
        }
    }

    public enum RotatingSide
    {
        None,
        Right,
        Left,
        Forward,
        Backward
    }
}