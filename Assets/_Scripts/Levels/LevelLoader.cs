using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Levels;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private int _levelForLoad;
    [SerializeField] private OverlayImage _overlayImage;
    [SerializeField] private LevelCounter _levelCounter; 

    [SerializeField] private bool _clearSaveMode;

    private int _currentLevel;
    private int _totalLevel;

    private void Start()
    {
#if UNITY_EDITOR
        if (_clearSaveMode)
        {
            SaveGame.Clear();
        }
        else
        {
            SetLevel();
        }
#endif
        _currentLevel = SaveGame.Load(Keys.CurrentLevel, 0);
        _totalLevel = SaveGame.Load(Keys.TotalLevel, 0);
        _levelCounter.Move(_totalLevel + 1);
        
        LoadLevel();
    }

    private void LoadLevel()
    {
        if (_levels.Count > 0)
        {
            foreach (var level in _levels)
                level.gameObject.SetActive(false);

            _levels[_currentLevel].gameObject.SetActive(true);

        }

        Time.timeScale = 1;
    }

    private void SetLevel()
    {
        if (_levelForLoad != 0)
            SaveGame.Save(Keys.CurrentLevel, _levelForLoad - 1);
    }

    public void LoadNextLevel()
    {
        _currentLevel++;
        _totalLevel++;

        if (_levels.Count <= _currentLevel)
            _currentLevel = 0;

        SaveGame.Save(Keys.TotalLevel, _totalLevel);
        SaveGame.Save(Keys.CurrentLevel, _currentLevel);

        StartCoroutine(RestartRoutine());
    }

    private IEnumerator RestartRoutine()
    {
        _overlayImage.FadeImage();
        yield return new WaitForSeconds(_overlayImage.FadeInDuration);
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        StartCoroutine(RestartRoutine());
    }
}
