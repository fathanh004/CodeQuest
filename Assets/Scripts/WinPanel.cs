using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    GameObject[] stars;

    [SerializeField]
    TMP_Text levelText;

    [SerializeField]
    TMP_Text timeText;

    public void SaveLevelData(int starCount, float time)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentStarCount = PlayerPrefs.GetInt(currentSceneName + "_Stars", 0);
        float currentTime = PlayerPrefs.GetFloat(currentSceneName + "_Time", 0);

        if (starCount > currentStarCount)
        {
            PlayerPrefs.SetInt(currentSceneName + "_Stars", starCount);
        }
        if (time < currentTime || currentTime == 0)
        {
            PlayerPrefs.SetFloat(currentSceneName + "_Time", time);
        }
    }

    public void ShowWinPanel(int starCount, float time)
    {
        levelText.text = SceneManager.GetActiveScene().name;
        timeText.text = string.Format("{0}:{1:00}", (int)time / 60, (int)time % 60);

        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
        }

        gameObject.SetActive(true);
        SaveLevelData(starCount, time);
    }

    public void NextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(currentSceneName.Substring(6));
        int nextLevel = currentLevel + 1;
        string nextSceneName = "Level " + nextLevel;
        SceneManager.LoadScene(nextSceneName);
    }
}
