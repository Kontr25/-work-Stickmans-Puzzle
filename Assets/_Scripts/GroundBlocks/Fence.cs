using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class Fence : MonoBehaviour
    {
        [SerializeField] private Transform _sentralPoint;

        public Transform SentralPoint
        {
            get => _sentralPoint;
            set => _sentralPoint = value;
        }
    }
}