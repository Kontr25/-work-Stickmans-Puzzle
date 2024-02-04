using System;
using UnityEngine;

namespace _Scripts.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _victory;
        
        public static Action VictorySound;

        private void OnEnable()
        {
            VictorySound += Victory;
        }

        private void OnDestroy()
        {
            VictorySound -= Victory;
        }

        private void Victory()
        {
            _victory.Play();
        }
    }
}