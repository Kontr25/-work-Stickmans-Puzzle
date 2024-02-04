using _Scripts.Environment;
using _Scripts.GameCamera;
using _Scripts.GroundBlocks;
using _Scripts.Levels;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GradientType _gradientType;
    [SerializeField] private int _maxStepsCount;
    [SerializeField] private float _cameraOrthoSize = 14f;
    [SerializeField] private BlockSkin _blockSkin;
    [SerializeField] private BlocksParent _blocksParent;
    [SerializeField] private EffectType _backgroundEffect;

    private void Start()
    {
        StepLimiter.Instance.RemainingSteps = _maxStepsCount;
        CameraController.Instance.OrthoSize(_cameraOrthoSize);
        _blocksParent.SetBlockSkin(_blockSkin);
        Background.Instance.SetBackgroundImage((int)_gradientType);
        Background.Instance.SetBackgoundEffects(_backgroundEffect);
    }
}
