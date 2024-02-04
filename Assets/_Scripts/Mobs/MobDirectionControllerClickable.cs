using _Scripts.Mobs.MovementConfigs;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobDirectionControllerClickable : MonoBehaviour
    {
        [SerializeField] private Camera _MainCamera;
        [SerializeField] private LayerMask _mobLayer;
        [SerializeField] private LayerMask _floorLayer;
        [SerializeField] private MobController _selectedMob;
        [SerializeField] private MobArrowClickable _arrow;
        
        private Vector3 _startPos;
        private Ray _ray;
        private RaycastHit _hit;

        public void MouseDown()
        {
            if (_selectedMob == null)
            {
                _ray = _MainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit, 1000, _mobLayer) &&
                    _hit.collider.gameObject.TryGetComponent(out MobController mobController) && !mobController.Mover.Mover.IsMovingNow)
                {
                    _selectedMob = mobController;
                    _selectedMob.Selected();
                    _startPos = _selectedMob.transform.position;
                }
                return;
            }
            else
            {
                MobDoMove();
            }
        }

        public void MobDoMove()
        {
            if(_selectedMob == null || _selectedMob.Mover.Mover.IsMovingNow) return;
            
            _ray = _MainCamera.ScreenPointToRay(Input.mousePosition);

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
                    _selectedMob.Rotate(RotatingSide.Right);
                }
                else
                {
                    _selectedMob.Rotate(RotatingSide.Left);
                }
            }
            else
            {
                if (direction.z > 0)
                {
                    _selectedMob.Rotate(RotatingSide.Forward);
                }
                else
                {
                    _selectedMob.Rotate(RotatingSide.Backward);
                }
            }
            
            _arrow.Enable(_selectedMob.transform.position, endPos, _selectedMob.transform);
            _selectedMob.DiSelected();

            _selectedMob = null;
        }
    }
}