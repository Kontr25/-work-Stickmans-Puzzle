using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.GroundBlocks
{
    public class BlocksParent : MonoBehaviour
    {
        [SerializeField] private List<BlockController> _blocks = new List<BlockController>();

        public void SetBlockSkin(BlockSkin skin)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out BlockController block))
                {
                    block.SetSkin(skin);
                }
            }
        }
    }
}