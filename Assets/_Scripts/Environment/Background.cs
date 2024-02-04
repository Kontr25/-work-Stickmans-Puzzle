using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Environment
{
    public class Background : MonoBehaviour
    {
        public static Background Instance;
        
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private Image _backgound;
        [SerializeField] private List<ParticleSystem> _backgroundEffects;

        private void Awake()
        {
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetBackgroundImage(int spriteNumber)
        {
            if (spriteNumber < _sprites.Count)
            {
                _backgound.sprite = _sprites[spriteNumber];
            }
        }

        public void SetBackgoundEffects(EffectType effectType)
        {
            _backgroundEffects[(int) effectType].Play();
        }
    }
}