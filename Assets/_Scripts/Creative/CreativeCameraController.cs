using System.Collections;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Creative
{
    public class CreativeCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _stickmanCamera;
        [SerializeField] private CinemachineVirtualCamera _defaultCamera;

        private void Start()
        {
            StartCoroutine(ActivateCamera());
        }

        private IEnumerator ActivateCamera()
        {
            yield return new WaitForSeconds(1f);
            _defaultCamera.Priority = 50;
            yield return new WaitForSeconds(2f);
            _stickmanCamera.Priority = 100;
        }
    }
}
