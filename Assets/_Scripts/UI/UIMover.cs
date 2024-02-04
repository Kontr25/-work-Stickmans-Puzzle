using DG.Tweening;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIMover : MonoBehaviour
    {
        [SerializeField] private Transform _targetPoints;
        [SerializeField] private float _moveDuration;

        public void Move()
        {
            transform.DOMove(_targetPoints.position, _moveDuration);
        }
    }
}