using System;
using _Scripts.Mobs;
using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class BlockPlace : MonoBehaviour
    {
        private bool _isBusy = false;
        private MobController _occupyingMob;

        public bool IsBusy
        {
            get => _isBusy;
            set => _isBusy = value;
        }

        public MobController OccupyingMob
        {
            get => _occupyingMob;
            set => _occupyingMob = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MobController mob) && !_isBusy)
            {
                _isBusy = true;
                _occupyingMob = mob;
            }else if (_isBusy)
            {
                mob.StepBack();
            }
        }
    }
}