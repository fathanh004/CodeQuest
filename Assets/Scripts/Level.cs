using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] int level;

    int starCount;

    float timePassed;

    [SerializeField] TMP_Text levelText;

    [SerializeField] GameObject[] stars;

    [SerializeField] Button playButton;

    private void Awake() {
        starCount = PlayerPrefs.GetInt("Level " + level + "_Stars", 0);
        timePassed = PlayerPrefs.GetFloat("Level " + level + "_Time", 0);
    }

    private void Start()
    {
        levelText.text = "Level " + level;
        playButton.onClick.AddListener(PlayLevel);

        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
        } 
    }

    public void PlayLevel()
    {
       SceneManager.LoadScene("Level " + level);
    }

}
