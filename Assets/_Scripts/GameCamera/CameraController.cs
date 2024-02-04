using System;
using Cinemachine;
using UnityEngine;

namespace _Scripts.GameCamera
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        [SerializeField] private CinemachineVirtualCamera _gameCamera;

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

        public void OrthoSize(float size)
        {
            _gameCamera.m_Lens.OrthographicSize = size;
        }
    }
}