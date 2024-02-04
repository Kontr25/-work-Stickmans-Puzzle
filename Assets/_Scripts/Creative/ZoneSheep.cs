using System;
using _Scripts.Mobs;
using UnityEngine;

namespace _Scripts.Creative
{
    public class ZoneSheep : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MobController mob))
            {
                _effect.Stop();
            }
        }
    }
}