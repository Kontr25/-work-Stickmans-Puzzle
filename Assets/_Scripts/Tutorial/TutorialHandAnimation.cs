using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Tutorial
{
    public class TutorialHandAnimation : MonoBehaviour
    {
        [SerializeField] private bool _clickTargetIsMob = false;
        [SerializeField] private Transform _handTransform;
        [SerializeField] private MeshRenderer _handMeshRenderer;
        [SerializeField] private float _spriteFadinTime;
        [SerializeField] private Transform _targetMobTransform;
        [SerializeField] private Transform _targetBlockTransform;
        [SerializeField] private float _clickDuration;

        private WaitForSeconds _clickDelay;
        private Coroutine _tutorialRoutine;
        private Vector3 _targetBlockPosition;
        private Vector3 _targetMobPosition;
        private Sequence _sequence;
        private Vector3 _defaultLocalPosition;
        private Vector3 _defaultBlockLocalPosition;

        private void Start()
        {
            _defaultLocalPosition = _handMeshRenderer.transform.localPosition;
            _defaultBlockLocalPosition = new Vector3(_handMeshRenderer.transform.localPosition.x,_handMeshRenderer.transform.localPosition.y-3,
                _handMeshRenderer.transform.localPosition.z);
            
            _targetBlockPosition = new Vector3(_targetBlockTransform.position.x, transform.position.y,
                _targetBlockTransform.position.z);
            _targetMobPosition = new Vector3(_targetMobTransform.position.x, transform.position.y,
                _targetMobTransform.position.z);

            _clickDelay = new WaitForSeconds(_spriteFadinTime * 2 + _clickDuration);
            
            EnableTutorial();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(gameObject);
            }
        }

        private void EnableTutorial()
        {
            DisableTutorial();
            _tutorialRoutine = StartCoroutine(TutorialRoutine());
        }

        private IEnumerator TutorialRoutine()
        {
            while (true)
            {
                transform.position = _targetMobPosition;
                ClickMob();
                yield return _clickDelay;
                _sequence.Kill();
                
                transform.position = _targetBlockPosition;
                if(!_clickTargetIsMob) ClickBlock();
                else ClickMob();
                yield return _clickDelay;
                _sequence.Kill();
                
                yield return _clickDelay;
            }
        }

        private void ClickMob()
        {
            _sequence = DOTween.Sequence();
            _handMeshRenderer.transform.localPosition = _defaultLocalPosition;
            _sequence.Append( _handMeshRenderer.material.DOFade(1, _spriteFadinTime));
            _sequence.Append(_handMeshRenderer.transform.DOLocalMoveY(_defaultLocalPosition.y - 1, _clickDuration/2));
            _sequence.Append(_handMeshRenderer.transform.DOLocalMoveY(_defaultLocalPosition.y, _clickDuration/2));
            _sequence.Append(_handMeshRenderer.material.DOFade(0, _spriteFadinTime));
        }
        
        private void ClickBlock()
        {
            _sequence = DOTween.Sequence();
            _handMeshRenderer.transform.localPosition = _defaultBlockLocalPosition;
            _sequence.Append( _handMeshRenderer.material.DOFade(1, _spriteFadinTime));
            _sequence.Append(_handMeshRenderer.transform.DOLocalMoveY(_defaultBlockLocalPosition.y - 1, _clickDuration/2));
            _sequence.Append(_handMeshRenderer.transform.DOLocalMoveY(_defaultBlockLocalPosition.y, _clickDuration/2));
            _sequence.Append(_handMeshRenderer.material.DOFade(0, _spriteFadinTime));
        }
        
        public void DisableTutorial()
        {
            if (_tutorialRoutine != null)
            {
                StopCoroutine(_tutorialRoutine);
                _tutorialRoutine = null;
            }
        }
    }
}