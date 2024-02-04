using TMPro;
using UnityEngine;

namespace _Scripts.Levels
{
    public class StepLimiter : MonoBehaviour
    {
        public static StepLimiter Instance;
        
        [SerializeField] private TMP_Text _remainingStepsText;

        private int _remainingSteps;
        private int _remainingActivatedBlocks;

        public int RemainingSteps
        {
            get => _remainingSteps;
            set
            {
                _remainingStepsText.text = value.ToString();
                _remainingSteps = value;
            }
        }

        public int RemainingActivatedBlocks
        {
            get => _remainingActivatedBlocks;
            set => _remainingActivatedBlocks = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                transform.parent = null;
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void BlockActivated()
        {
            RemainingActivatedBlocks--;
            if (AllBlockActivated())
            {
                FinishAction.Finish.Invoke(FinishAction.FinishType.Win);
            }
        }

        public void Step()
        {
            RemainingSteps--;
            if (StepsCountOver() && !AllBlockActivated())
            {
                FinishAction.Finish.Invoke(FinishAction.FinishType.Lose);
            }
        }

        private bool StepsCountOver()
        {
            if (RemainingSteps <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool AllBlockActivated()
        {
            if (RemainingActivatedBlocks <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}