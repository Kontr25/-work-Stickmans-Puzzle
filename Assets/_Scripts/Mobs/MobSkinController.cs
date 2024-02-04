using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Mobs
{
    public class MobSkinController : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
        
        [SerializeField] private List<GameObject> _warriorItems;
        [SerializeField] private List<GameObject> _intelegentItems;
        [SerializeField] private List<GameObject> _ninjaRunnerItems;
        [SerializeField] private List<GameObject> _ninjaJumperItems;
        [SerializeField] private List<GameObject> _magItems;

        [SerializeField] private Material _warriorMaterial;
        [SerializeField] private Material _intelegentMaterial;
        [SerializeField] private Material _ninjaRunnerMaterial;
        [SerializeField] private Material _ninjaJumperMaterial;
        [SerializeField] private Material _magMaterial;

        public void SetSkin(MobType type)
        {
            DeactivateAll();
            switch (type)
            {
                case MobType.OneStepMovement:
                    _skinnedMesh.material = _warriorMaterial;
                    ActivateItems(_warriorItems);
                    break;
                case MobType.DoubleStepMovement:
                    _skinnedMesh.material = _intelegentMaterial;
                    ActivateItems(_intelegentItems);
                    break;
                case MobType.JumpOneStepMovement:
                    _skinnedMesh.material = _ninjaJumperMaterial;
                    ActivateItems(_ninjaJumperItems);
                    break;
                case MobType.UnlimitedStepMovement:
                    _skinnedMesh.material = _ninjaRunnerMaterial;
                    ActivateItems(_ninjaRunnerItems);
                    break;
                case MobType.GlideTwoStepMovement:
                    _skinnedMesh.material = _magMaterial;
                    ActivateItems(_magItems);
                    break;
            }
        }

        private void ActivateItems(List<GameObject> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetActive(true);
            }
        }

        private void DeactivateAll()
        {
            DeactivateItems(_warriorItems);
            DeactivateItems(_intelegentItems);
            DeactivateItems(_ninjaRunnerItems);
            DeactivateItems(_ninjaJumperItems);
            DeactivateItems(_magItems);
        }
        
        private void DeactivateItems(List<GameObject> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetActive(false);
            }
        }
    }
}