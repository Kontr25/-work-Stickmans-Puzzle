using System;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs.MovementConfigs
{
    public class MobArrowClickable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _arrowSprite;
        [SerializeField] private float _enableDuration;

        private Transform _target;
        private bool _isEnabled = false;
        private Vector3 _startPos;
        private Vector3 _endPos;
        private Quaternion _targetRotation;
        private Sequence _sequence;

        private void Update()
        {
            if(!_isEnabled) return;

            if (_target != null)
            {
                Vector3 target = new Vector3(_target.position.x,transform.position.y, _target.position.z);
                transform.position = target;
            }
        }

        public void Enable(Vector3 startPosition, Vector3 endPosition, Transform target)
        {
            transform.position = startPosition;
            _startPos = startPosition;
            _endPos = endPosition;
            _isEnabled = true;
            _target = target;
            if (_sequence != null)
            {
                _sequence.Kill();
                _arrowSprite.DOFade(0, 0);
            }
            
            Vector3 direction = _endPos - _startPos;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                if (direction.x > 0)
                {
                    _targetRotation = Quaternion.Euler(0, 90, 0);
                }
                else
                {
                    _targetRotation = Quaternion.Euler(0, 270, 0);
                }
            }
            else
            {
                if (direction.z > 0)
                {
                    _targetRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    _targetRotation = Quaternion.Euler(0, 180, 0);
                }
            }
            
            transform.rotation = _targetRotation;

            _sequence = DOTween.Sequence();
            _sequence.Append(_arrowSprite.DOFade(1f, _enableDuration));
            _sequence.Append(_arrowSprite.DOFade(0, _enableDuration)).onComplete = () =>
            {
                _isEnabled = false;
            };
        }
    }
}