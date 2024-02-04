using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class SelectionView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _selectedSprite;
        [SerializeField] private float _selectedShowTime;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private List<Outline> _outlines;

        private Color _defaultColor;
        private Sequence _sequence;

        private void Start()
        {
            _defaultColor = _outlines[0].OutlineColor;
        }

        public void Selected()
        {
            for (int i = 0; i < _outlines.Count; i++)
            {
                 _outlines[i].OutlineColor = _selectedColor;
                 _outlines[i].OutlineWidth = 5;
            }
           
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            if (_sequence != null)
            {
                _sequence.Kill();
            }
            _sequence = DOTween.Sequence();
            _sequence.Append(_selectedSprite.DOFade(.75f, _selectedShowTime));
            _sequence.Insert(0,
                transform.DORotateQuaternion(Quaternion.Euler(0, 270, 0), _selectedShowTime));
            _sequence.Insert(_selectedShowTime,_selectedSprite.DOFade(0, _selectedShowTime));
            _sequence.Insert(0,transform.DOScale(Vector3.one * 1.2f,
                1f / 3f * _selectedShowTime));
            _sequence.Insert( 1f / 3f * _selectedShowTime, transform.DOScale(0.4f,
                    2f / 3f * _selectedShowTime));
        }

        public void DiSelected()
        {
            for (int i = 0; i < _outlines.Count; i++)
            {
                _outlines[i].OutlineColor = _defaultColor;
                _outlines[i].OutlineWidth = 2;
            }
        }
    }
}