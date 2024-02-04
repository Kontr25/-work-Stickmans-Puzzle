using UnityEngine;

namespace _Scripts.Creative
{
    public abstract class TrapBase: MonoBehaviour
    {
        [SerializeField] protected Transform ExplosionPoint;
        [SerializeField] protected float ExplosionForce;
        [SerializeField] protected float ExplosionRadius;
    }
}