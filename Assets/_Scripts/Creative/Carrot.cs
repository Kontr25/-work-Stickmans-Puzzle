using _Scripts.Mobs;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Creative
{
    public class Carrot : MonoBehaviour
    {
        [SerializeField] private Transform _meshTransform;
        [SerializeField] private ParticleSystem _effect;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MobController mob))
            {
                Eat();
            }
        }

        private void Eat()
        {
            _effect.Play();
            _meshTransform.DOScale(0, .2f);
        }
    }
}