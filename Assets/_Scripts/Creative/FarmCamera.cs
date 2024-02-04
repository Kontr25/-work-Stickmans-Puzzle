using _Scripts.Mobs;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Creative
{
    public class FarmCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _farmCamera;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MobController mob))
            {
                _farmCamera.Priority = 200;
            }
        }
    }
}