using _Scripts.Mobs;
using UnityEngine;

namespace _Scripts.Creative
{
    public class TrapBaseShip : TrapBase
    {
        [SerializeField] private Animator _animator;
        private static readonly int Ship = Animator.StringToHash("Ship");

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MobController mob))
            {
                KillMob(mob);
            }
        }

        private void KillMob(MobController mob)
        {
            _animator.SetTrigger(Ship);
            mob.DieOnTrap(ExplosionPoint.position, ExplosionForce* 50, ExplosionRadius);
        }
    }
}