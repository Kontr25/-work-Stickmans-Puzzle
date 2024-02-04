using System.Collections.Generic;
using _Scripts.Mobs;
using UnityEngine;

namespace _Scripts.Creative
{
    public class BlockSkinChanger : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private List<Material> _materials;
        [SerializeField] private List<GameObject> _items;
        [SerializeField] private ParticleSystem _changeEffect;
        [SerializeField] private MobType _mobType;

        public MobType ChangeMobType
        {
            get => _mobType;
            set => _mobType = value;
        }

        private void Start()
        {
            Material[] materials = _meshRenderer.materials;
            materials[1] = _materials[(int) _mobType];
            _meshRenderer.materials = materials; 
            _items[(int)_mobType].SetActive(true);
        }

        public void Change()
        {
            _changeEffect.Play();
        }
    }
}