using System.Collections;
using _Scripts.Mobs;
using UnityEngine;

namespace _Scripts.Creative
{
    public class TrapBaseSpring : TrapBase
    {
        [SerializeField] private Rigidbody _fallingPlatform;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MobController mob))
            {
                ExplosionPoint.SetParent(null);
                StartCoroutine(KillMob(mob));
            }
        }

        private IEnumerator KillMob(MobController mob)
        {
            yield return new WaitForSeconds(1f);
            _fallingPlatform.isKinematic = false;
            _fallingPlatform.transform.SetParent(null);
            _fallingPlatform.AddExplosionForce(ExplosionForce, ExplosionPoint.position, ExplosionRadius);
            _fallingPlatform.angularVelocity = new Vector3(1, 0, 0);
            mob.DieOnTrap(ExplosionPoint.position, ExplosionForce* 50, ExplosionRadius);
        }
    }
}