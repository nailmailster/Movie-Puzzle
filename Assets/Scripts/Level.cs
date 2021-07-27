using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;

    [SerializeField] TextMeshProUGUI headingText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] Image imageGold, imageSilver;
    [SerializeField] GameObject mainMenu, acknowledgementsMenu;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Win")
        {
            int totalTime = PlayerPrefs.GetInt("totalTime");
            int totalTimeInDecimals = PlayerPrefs.GetInt("totalTimeInDecimals");
            totalTime += totalTimeInDecimals / 10;
            totalTimeInDecimals = totalTimeInDecimals % 10;
            int hours = totalTime / 3600;
            totalTime = totalTime % 3600;
            int minutes = totalTime / 60;
            int seconds = totalTime % 60;
            totalScoreText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + totalTimeInDecimals.ToString("D1");

            if (PlayerPrefs.GetInt("highScore") == 1)
            {
                headingText.text = "C<size=85%>HAMPION</size>!";
                imageSilver.enabled = false;
                imageGold.enabled = true;
            }
            else
            {
                headingText.text = "Y<size=85%>OU</size> W<size=85%>IN</size>!";
                imageGold.enabled = false;
                imageSilver.enabled = true;
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("level1BestTime") == 0)
                PlayerPrefs.SetInt("level1BestTime", 7777777);
            if (PlayerPrefs.GetInt("level1BestTimeInDecimals") == 0)
                PlayerPrefs.SetInt("level1BestTimeInDecimals", 7777777);
            if (PlayerPrefs.GetInt("level2BestTime") == 0)
                PlayerPrefs.SetInt("level2BestTime", 7777777);
            if (PlayerPrefs.GetInt("level2BestTimeInDecimals") == 0)
                PlayerPrefs.SetInt("level2BestTimeInDecimals", 7777777);
            if (PlayerPrefs.GetInt("level3BestTime") == 0)
                PlayerPrefs.SetInt("level3BestTime", 7777777);
            if (PlayerPrefs.GetInt("level3BestTimeInDecimals") == 0)
                PlayerPrefs.SetInt("level3BestTimeInDecimals", 7777777);
            if (PlayerPrefs.GetInt("level4BestTime") == 0)
                PlayerPrefs.SetInt("level4BestTime", 7777777);
            if (PlayerPrefs.GetInt("level4BestTimeInDecimals") == 0)
                PlayerPrefs.SetInt("level4BestTimeInDecimals", 7777777);
            if (PlayerPrefs.GetInt("level5BestTime") == 0)
                PlayerPrefs.SetInt("level5BestTime", 7777777);
            if (PlayerPrefs.GetInt("level5BestTimeInDecimals") == 0)
                PlayerPrefs.SetInt("level5BestTimeInDecimals", 7777777);
            if (PlayerPrefs.GetInt("totalBestTime") == 0)
                PlayerPrefs.SetInt("totalBestTime", 7777777);
            if (PlayerPrefs.GetInt("totalBestTimeInDecimals") == 0)
                PlayerPrefs.SetInt("totalBestTimeInDecimals", 7777777);
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
        // if (Application.platform == RuntimePlatform.Android)
        // {
        //     AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        //     activity.Call<bool>("moveTaskToBack", true);
        // }
        // else
        // {
        //     Application.Quit();
        // }
    }

    public void TryAgain()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Debug.Log(SceneManager.GetActiveScene().buildIndex);
            // if (SceneManager.GetActiveScene().buildIndex == 0)
                Application.Quit();
            // else// if (SceneManager.GetActiveScene().buildIndex == 1)
            //     SceneManager.LoadScene("Start Menu");
        }
    }

    public void ResetScores()
    {
        PlayerPrefs.SetInt("level1BestTime", 7777777);
        PlayerPrefs.SetInt("level2BestTime", 7777777);
        PlayerPrefs.SetInt("level3BestTime", 7777777);
        PlayerPrefs.SetInt("level4BestTime", 7777777);
        PlayerPrefs.SetInt("level5BestTime", 7777777);
        PlayerPrefs.SetInt("totalBestTime", 7777777);
    }
}
