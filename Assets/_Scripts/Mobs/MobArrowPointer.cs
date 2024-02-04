using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobArrowPointer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _arrowSprite;
        [SerializeField] private float _enableDuration;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _floorLayer;

        private bool _isEnabled = false;
        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _startPos;
        private Quaternion _targetRotation;

        public void Enable(Vector3 mobPosition)
        {
            transform.position = mobPosition;
            _startPos = transform.position;
            _arrowSprite.DOFade(1f, _enableDuration);
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
            _arrowSprite.DOFade(0, _enableDuration);
        }
        
        private void FixedUpdate()
        {
            if(!_isEnabled) return;
            
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 endPos = new Vector3();
            if (Physics.Raycast(_ray, out _hit, 1000, _floorLayer))
            {
                endPos = _hit.point;
            }

            Vector3 direction = endPos - _startPos;
            
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
        }
    }
}