using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobDisappearer : MonoBehaviour
    {
        [SerializeField] private Transform _mobTransform;
        [SerializeField] private float _smallScale;
        [SerializeField] private float _disappearDuration;
        public void Disappear()
        {
            _mobTransform.DOLocalMove(Vector3.zero, _disappearDuration);
            _mobTransform.DOScale(_smallScale, _disappearDuration).onComplete = () =>
            {
                
                Destroy(_mobTransform.gameObject);
            };
        }
    }
}