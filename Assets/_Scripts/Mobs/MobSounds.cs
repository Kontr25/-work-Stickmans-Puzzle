using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _screech;
        [SerializeField] private List<AudioClip> _footstepSounds;
        [SerializeField] private List<AudioClip> _stumblingSounds;
        [SerializeField] private AudioSource _footstepAudioSource;
        [SerializeField] private AudioSource _stumblingAudioSource;
        [SerializeField] private AudioSource _jumpAudioSource;
        [SerializeField] private AudioSource _indentedJumpAudioSource;

        public void PlayFootstepSound()
        {
            AudioClip footstepSound = _footstepSounds[Random.Range(0, _footstepSounds.Count)];
            _footstepAudioSource.PlayOneShot(footstepSound);
        }

        public void PlayStumblingSound()
        {
            AudioClip stumblingSound = _stumblingSounds[Random.Range(0, _stumblingSounds.Count)];
            _stumblingAudioSource.PlayOneShot(stumblingSound);
        }
        public void Screech()
        {
            _screech.Play();
        }

        public void PlayJumpSound()
        {
            _jumpAudioSource.Play();
        }
        
        public void PlayIndentedJumpSound()
        {
            _indentedJumpAudioSource.Play();
        }
    }
}