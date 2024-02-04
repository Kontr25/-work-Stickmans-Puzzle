using System;
using _Scripts.Creative;
using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class MobStopPoint : MonoBehaviour
    {
        [SerializeField] private Transform _stabilisePoint;
        [SerializeField] private bool _isFailPoint = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BlockController block) || other.TryGetComponent(out TrapBase trap) 
                                                                 || other.TryGetComponent(out BlockSkinChanger skinChanger))
            {
                IsFailPoint = false;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BlockController block) || other.TryGetComponent(out TrapBase trap)
                                                                 || other.TryGetComponent(out BlockSkinChanger skinChanger))
            {
                IsFailPoint = true;
            }
        }

        public Transform StabilisePoint
        {
            get => _stabilisePoint;
            set => _stabilisePoint = value;
        }

        public bool IsFailPoint
        {
            get => _isFailPoint;
            set => _isFailPoint = value;
        }
    }
}