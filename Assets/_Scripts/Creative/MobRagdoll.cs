using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Creative
{
    public class MobRagdoll : MonoBehaviour
    {
        [SerializeField] private Rigidbody _pelvis;
        [SerializeField] private List<Rigidbody> _ragdollParts;
        [SerializeField] private List<Collider> _ragdollColliders;

        public void EnableRagdoll(Vector3 explosionPosition, float explosionForce, float explosionRadius)
        {
            for (int i = 0; i < _ragdollParts.Count; i++)
            {
                _ragdollParts[i].isKinematic = false;
                _ragdollColliders[i].isTrigger = false;
            }
            
            _pelvis.AddExplosionForce(explosionForce, explosionPosition, explosionRadius * 1.7f);
        }
    }
}