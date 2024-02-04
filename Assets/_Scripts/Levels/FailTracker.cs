using _Scripts.Mobs;
using UnityEngine;

namespace _Scripts.Levels
{
    public class FailTracker : MonoBehaviour
    {
        [SerializeField] private LevelLoader _levelLoader;
        private bool _fail = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MobController mob) && !_fail)
            {
                _fail = true;
                FinishAction.Finish.Invoke(FinishAction.FinishType.Lose);
            }
        }
    }
}