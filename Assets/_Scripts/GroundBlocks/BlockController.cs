using System;
using System.Collections.Generic;
using _Scripts.Levels;
using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private BlockSideType blockSideType;
        [SerializeField] private BlockAnimator _blockAnimator;
        [SerializeField] private ActivatedBlockSide _activatedBlockSide;
        [SerializeField] private GameObject _star;
        [SerializeField] private List<GameObject> _skins;

        private void Awake()
        {
            SetType();
        }

        private void SetType()
        {
            switch (blockSideType)
            {
                case BlockSideType.None:
                    _blockAnimator.NotAvtivatedRotation();
                    break;
                case BlockSideType.ActivatedType:
                    _blockAnimator.ActivatedRotation();
                    _star.SetActive(true);
                    StepLimiter.Instance.RemainingActivatedBlocks++;
                    break;
                case BlockSideType.NotActivatedType:
                    _blockAnimator.NotAvtivatedRotation();
                    break;
            }
        }

        public void Rotate()
        {
            _blockAnimator.Rotate();
            StepLimiter.Instance.BlockActivated();
            StepLimiter.Instance.Step();
        }

        public void SetSkin(BlockSkin skin)
        {
            for (int i = 0; i < _skins.Count; i++)
            {
                _skins[i].SetActive(false);
            }
            _skins[(int)skin].SetActive(true);
            _activatedBlockSide.BlockType = skin;
        }
    }
}