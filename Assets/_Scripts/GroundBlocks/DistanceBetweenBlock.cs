using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class DistanceBetweenBlock : MonoBehaviour
    {
        [SerializeField] private Transform _firstBlock;
        [SerializeField] private Transform _secondBlock;

        private float _distance;
        public static DistanceBetweenBlock Instance;

        public float Distance => _distance;

        private void Awake()
        {
            if (Instance == null)
            {
                transform.parent = null;
                Instance = this;
                
            }
            else
            {
                Destroy(gameObject);
            }
            
            _distance = Vector3.Distance(_firstBlock.position, _secondBlock.position);
        }
    }
}