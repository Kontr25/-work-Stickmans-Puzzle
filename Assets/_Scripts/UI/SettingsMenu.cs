using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _closePanel;
    [SerializeField] private Transform _defaultButtonsPosition;
    [SerializeField] private float _openTime = 1f;
    
    [Header("Buttons")]
    [SerializeField] private Button _soundButton;
    [SerializeField] private Sprite _enabledSoundSprite;
    [SerializeField] private Sprite _disabledSoundSprite;

    [SerializeField] private Image[] _buttonImages;
    [SerializeField] private TMP_Text[] _buttonTexts;
    
    [SerializeField] private Button _ppButton;
    [Space]
    
    [Header("Points")]
    [SerializeField] private List<Transform> _pointsForButtons;

    private bool _fpsEnabled = false;
    private int _attempt;
    private bool _isOpen = false;
    private bool _volumeEnable = true;
    private bool _vibroEnable = true;


    public void SwitchSettingsMenu()
    {
        if (_isOpen == false)
        {
            for (int i = 0; i < _buttonImages.Length; i++)
            {
                _buttonImages[i].DOFade(1f, _openTime - .4f);
            }

            for (int i = 0; i < _buttonTexts.Length; i++)
            {
                _buttonTexts[i].DOFade(1f, _openTime - .4f);
            }
            
            _soundButton.transform.DOMove(_pointsForButtons[0].transform.position, _openTime);
            _ppButton.transform.DOMove(_pointsForButtons[1].transform.position, _openTime);
            _closePanel.SetActive(true);
        }
        else
        {
            for (int i = 0; i < _buttonImages.Length; i++)
            {
                _buttonImages[i].DOFade(0f, _openTime - .4f);
            }

            for (int i = 0; i < _buttonTexts.Length; i++)
            {
                _buttonTexts[i].DOFade(0f, _openTime - .4f);
            }
            
            _soundButton.transform.DOMove(_defaultButtonsPosition.position, _openTime);
            _ppButton.transform.DOMove(_defaultButtonsPosition.position, _openTime);
            _closePanel.SetActive(false);
        }

        _isOpen = !_isOpen;
    }

    public void SwitchSound()
    {
        _volumeEnable = !_volumeEnable;

        if (_volumeEnable == false)
        {
            AudioListener.volume = 0f;
            _soundButton.image.sprite = _disabledSoundSprite;
        }
        else
        {
            AudioListener.volume = 1f;
            _soundButton.image.sprite = _enabledSoundSprite;
        }
    }

    public void ShowPolicy()
    {
        Application.OpenURL("https://ink-quokka-842.notion.site/Privacy-Policy-e2837eafdbe04769b7f913ebef507164");
    }
}