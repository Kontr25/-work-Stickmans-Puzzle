using System.Collections.Generic;
using _Scripts.Mobs;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class ActivatedBlockSide : MonoBehaviour
    {
        [SerializeField] private BlockController _blockController;
        [SerializeField] private Transform _rotatedBlockTransform;
        [SerializeField] private MeshRenderer _starMeshRenderer;
        [SerializeField] private Transform _star;
        [SerializeField] private float _disappearTime;
        [SerializeField] private float _starRiseHeight;
        [SerializeField] private ParticleSystem _starExplosion;
        [SerializeField] private List<AudioSource> _blockSound;
        
        private BlockSkin _blockType;
        private bool _activated = false;

        public BlockSkin BlockType
        {
            get => _blockType;
            set => _blockType = value;
        }

        public void Activate(MobController mob)
        {
            if(_activated) return;
            _activated = true;
            _starMeshRenderer.material.DOFade(0, _disappearTime);
            _star.DOScale(_star.localScale * 3, _disappearTime);
            _star.DOLocalMoveY(_star.localPosition.y + _starRiseHeight, _disappearTime);
            _starExplosion.Play();
            mob.Disappear(_rotatedBlockTransform);
            _blockController.Rotate();
            _blockSound[(int)_blockType].Play();
        }
    }
}