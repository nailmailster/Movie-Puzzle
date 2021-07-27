using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Squares : MonoBehaviour
{
    [SerializeField] int size;
    [SerializeField] GameObject[] squares3x3 = new GameObject[9];
    Vector2[] positions3x3 = new Vector2[]
                        {
                            new Vector2(-1.65f, 1.85f),
                            new Vector2(0f, 1.85f),
                            new Vector2(1.65f, 1.85f),

                            new Vector2(-1.65f, 0.6f),
                            new Vector2(0f, 0.6f),
                            new Vector2(1.65f, 0.6f),

                            new Vector2(-1.65f, -0.65f),
                            new Vector2(0f, -0.65f),
                            new Vector2(1.65f, -0.65f)
                        };
    Vector2[] stickedPositions3x3 = new Vector2[]
                        {
                            new Vector2(-1.6f, 1.8f),
                            new Vector2(0f, 1.8f),
                            new Vector2(1.6f, 1.8f),

                            new Vector2(-1.6f, 0.6f),
                            new Vector2(0f, 0.6f),
                            new Vector2(1.6f, 0.6f),

                            new Vector2(-1.6f, -0.6f),
                            new Vector2(0f, -0.6f),
                            new Vector2(1.6f, -0.6f)
                        };
    
    [SerializeField] GameObject[] squares4x4 = new GameObject[16];
    Vector2[] positions4x4 = new Vector2[]
                        {
                            new Vector2(-1.84f, 2f),
                            new Vector2(-0.61f, 2f),
                            new Vector2(0.62f, 2f),
                            new Vector2(1.85f, 2f),

                            new Vector2(-1.84f, 1.07f),
                            new Vector2(-0.61f, 1.07f),
                            new Vector2(0.62f, 1.07f),
                            new Vector2(1.85f, 1.07f),

                            new Vector2(-1.84f, 0.14f),
                            new Vector2(-0.61f, 0.14f),
                            new Vector2(0.62f, 0.14f),
                            new Vector2(1.85f, 0.14f),

                            new Vector2(-1.84f, -0.79f),
                            new Vector2(-0.61f, -0.79f),
                            new Vector2(0.62f, -0.79f),
                            new Vector2(1.85f, -0.79f),
                        };
    Vector2[] stickedPositions4x4 = new Vector2[]
                        {
                            new Vector2(-1.79f, 1.88f),
                            new Vector2(-0.61f, 1.88f),
                            new Vector2(0.57f, 1.88f),
                            new Vector2(1.75f, 1.88f),

                            new Vector2(-1.79f, 1.01f),
                            new Vector2(-0.61f, 1.01f),
                            new Vector2(0.57f, 1.01f),
                            new Vector2(1.75f, 1.01f),

                            new Vector2(-1.79f, 0.14f),
                            new Vector2(-0.61f, 0.14f),
                            new Vector2(0.57f, 0.14f),
                            new Vector2(1.75f, 0.14f),

                            new Vector2(-1.79f, -0.73f),
                            new Vector2(-0.61f, -0.73f),
                            new Vector2(0.57f, -0.73f),
                            new Vector2(1.75f, -0.73f),
                        };

    [SerializeField] GameObject[] squares4x3 = new GameObject[12];
    Vector2[] positions4x3 = new Vector2[]
                        {
                            new Vector2(-1.84f, 1.85f),
                            new Vector2(-0.61f, 1.85f),
                            new Vector2(0.62f, 1.85f),
                            new Vector2(1.85f, 1.85f),

                            new Vector2(-1.84f, 0.6f),
                            new Vector2(-0.61f, 0.6f),
                            new Vector2(0.62f, 0.6f),
                            new Vector2(1.85f, 0.6f),

                            new Vector2(-1.84f, -0.65f),
                            new Vector2(-0.61f, -0.65f),
                            new Vector2(0.62f, -0.65f),
                            new Vector2(1.85f, -0.65f),
                        };
    Vector2[] stickedPositions4x3 = new Vector2[]
                        {
                            new Vector2(-1.79f, 1.8f),
                            new Vector2(-0.61f, 1.8f),
                            new Vector2(0.57f, 1.8f),
                            new Vector2(1.75f, 1.8f),

                            new Vector2(-1.79f, 0.6f),
                            new Vector2(-0.61f, 0.6f),
                            new Vector2(0.57f, 0.6f),
                            new Vector2(1.75f, 0.6f),

                            new Vector2(-1.79f, -0.6f),
                            new Vector2(-0.61f, -0.6f),
                            new Vector2(0.57f, -0.6f),
                            new Vector2(1.75f, -0.6f),
                        };

    [SerializeField] TextMeshProUGUI description, author1, author2;
    
    public int selectedCell = -1;
    public GameObject selectedSquare;

    // [SerializeField] GameObject canvas;

    UnityEngine.Video.VideoPlayer[] videoPlayers = new UnityEngine.Video.VideoPlayer[16];

    int FileIDDrop, SoundIDDrop;
    int FileIDBong, SoundIDBong;
    static int musicID, applauseID;

    [SerializeField] TextMeshProUGUI timerText;
    int hours = 0, minutes = 0, seconds = 0, hundreds = 0;
    float timer = 0f;

    bool videoIsReady;

    [SerializeField] TextMeshProUGUI bestTimeText;
    [SerializeField] TextMeshProUGUI loadingText;

    [SerializeField] Toggle soundToggle, sfxToggle, assistantToggle;

    [SerializeField] ParticleSystem heart;

    void Start()
    {
        int[] freeCells3x3 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        int[] freeCells4x4 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        int[] freeCells4x3 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        int random;

        bestTimeText.enabled = false;
        bestTimeText.gameObject.SetActive(false);

        videoIsReady = false;

        if (size == 9)
        {
            for (int i = 0; i < 9; i++)
            {
                random = -1;

                while (random == -1)
                {
                    random = Random.Range(0, 9);
                    random = freeCells3x3[random];
                    if (random != -1)
                    {
                        freeCells3x3[random] = -1;
                        squares3x3[i].transform.position = positions3x3[random];
                    }
                    else
                        continue;
                }

                videoPlayers[i] = squares3x3[i].GetComponent<UnityEngine.Video.VideoPlayer>();
            }
        }
        else if (size == 16)
        {
            for (int i = 0; i < 16; i++)
            {
                random = -1;

                while (random == -1)
                {
                    random = Random.Range(0, 16);
                    random = freeCells4x4[random];
                    if (random != -1)
                    {
                        freeCells4x4[random] = -1;
                        squares4x4[i].transform.position = positions4x4[random];
                    }
                    else
                        continue;
                }

                videoPlayers[i] = squares4x4[i].GetComponent<UnityEngine.Video.VideoPlayer>();
            }
        }
        else if (size == 12)
        {
            for (int i = 0; i < 12; i++)
            {
                random = -1;

                while (random == -1)
                {
                    random = Random.Range(0, 12);
                    random = freeCells4x3[random];
                    if (random != -1)
                    {
                        freeCells4x3[random] = -1;
                        squares4x3[i].transform.position = positions4x3[random];
                    }
                    else
                        continue;
                }

                videoPlayers[i] = squares4x3[i].GetComponent<UnityEngine.Video.VideoPlayer>();
            }
        }

        // canvas.SetActive(false);
        SettingsData.Win = false;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            description.text = "\"THE DAILY DWEEBS\"";
            author1.text = "BY BLENDER FOUNDATION, 2017";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            description.text = "\"CAMINANDES 3: LLAMIGOS\"";
            author1.text = "BY BLENDER FOUNDATION, 2016";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            description.text = "\"SPRING\"";
            author1.text = "BY BLENDER FOUNDATION, 2019";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            description.text = "\"SINTEL\"";
            author1.text = "BY BLENDER FOUNDATION, 2010";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            description.text = "\"BIG BUCK BUNNY\"";
            author1.text = "BY BLENDER FOUNDATION, 2008";
        }
        author2.text = "(CC-BY LICENSE)";

        LoadVideo();
        loadingText.gameObject.layer = 10;
        loadingText.enabled = true;
        loadingText.gameObject.SetActive(true);
        StartCoroutine("PrepareVideo");

        AndroidNativeAudio.makePool(1);
        FileIDDrop = AndroidNativeAudio.load("Android Native Audio/drop_003.ogg");
        FileIDBong = AndroidNativeAudio.load("Android Native Audio/drop_004.ogg");
        // if (SceneManager.GetActiveScene().buildIndex == 1)
        // {
            musicID = ANAMusic.load("Android Native Audio/Mysterious-Puzzle_Looping.ogg");
            applauseID = ANAMusic.load("Android Native Audio/applause_mono.ogg");
            ANAMusic.setLooping(musicID, true);
            ANAMusic.setLooping(applauseID, false);

            if (!SettingsData.SoundOn)
            {
                ANAMusic.setVolume(musicID, 0f);
                ANAMusic.setVolume(applauseID, 0f);
                soundToggle.isOn = false;
            }
            else
            {
                ANAMusic.setVolume(musicID, 0.5f);
                ANAMusic.setVolume(applauseID, 0.5f);
                soundToggle.isOn = true;
            }

            if (SettingsData.SfxOn)
                sfxToggle.isOn = true;
            else
                sfxToggle.isOn = false;
            
            // assistantToggle.isOn = false;
            // SettingsData.AssistantOn = false;

            ANAMusic.play(musicID);
        // }
    }

    void Update()
    {
        // if (!canvas.activeInHierarchy && videoIsReady)
        if (!SettingsData.Win && videoIsReady)
        {
            timer += Time.deltaTime;
            // if (timer >= 1f)
            // {
            //     seconds++;
            //     if (seconds == 60)
            //     {
            //         seconds = 0;
            //         minutes++;
            //         if (minutes == 60)
            //         {
            //             minutes = 0;
            //             hours++;
            //         }
            //     }
            //     timerText.text = "<size=-20>YOUR TIME</size> <#ddee00>" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "</color>";
            //     timer = 0f;
            // }
            if (timer >= .1f)
            {
                hundreds += (int)(timer * 10f);
                if (hundreds >= 10)
                {
                    seconds++;
                    hundreds -= 10;
                }
                if (seconds == 60)
                {
                    seconds = 0;
                    minutes++;
                    if (minutes == 60)
                    {
                        minutes = 0;
                        hours++;
                    }
                }
                timerText.text = "<size=-20>YOUR TIME</size> <#ddee00>" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + hundreds.ToString("D1") + "</color>";
                timer = 0f;
            }
        }
    }

    public bool CheckWin()
    {
        if (size == 9)
        {
            if (squares3x3[0].transform.position.x == -1.65f && squares3x3[0].transform.position.y == 1.85f
                && squares3x3[1].transform.position.x == 0 && squares3x3[1].transform.position.y == 1.85f
                && squares3x3[2].transform.position.x == 1.65f && squares3x3[2].transform.position.y == 1.85f

                && squares3x3[3].transform.position.x == -1.65f && squares3x3[3].transform.position.y == 0.6f
                && squares3x3[4].transform.position.x == 0 && squares3x3[4].transform.position.y == 0.6f
                && squares3x3[5].transform.position.x == 1.65f && squares3x3[5].transform.position.y == 0.6f

                && squares3x3[6].transform.position.x == -1.65f && squares3x3[6].transform.position.y == -0.65f
                && squares3x3[7].transform.position.x == 0 && squares3x3[7].transform.position.y == -0.65f
                && squares3x3[8].transform.position.x == 1.65f && squares3x3[8].transform.position.y == -0.65f)
            {
                // canvas.SetActive(true);
                SettingsData.Win = true;
                SaveBestTime();

                for (int i = 0; i < 9; i++)
                {
                    squares3x3[i].transform.position = stickedPositions3x3[i];
                    //  iTween здесь не работает
                    // iTween.MoveTo(squares[i], iTween.Hash(
                    //     "position", stickedPositions[i],
                    //     "time", .3f
                    // ));
                }

                PlayAudio(4);
                return true;
            }
            else
                return false;
        }
        else if (size == 16)
        {
            if (squares4x4[0].transform.position.x == -1.84f && squares4x4[0].transform.position.y == 2f
                && squares4x4[1].transform.position.x == -0.61f && squares4x4[1].transform.position.y == 2f
                && squares4x4[2].transform.position.x == 0.62f && squares4x4[2].transform.position.y == 2f
                && squares4x4[3].transform.position.x == 1.85f && squares4x4[3].transform.position.y == 2f

                && squares4x4[4].transform.position.x == -1.84f && squares4x4[4].transform.position.y == 1.07f
                && squares4x4[5].transform.position.x == -0.61f && squares4x4[5].transform.position.y == 1.07f
                && squares4x4[6].transform.position.x == 0.62f && squares4x4[6].transform.position.y == 1.07f
                && squares4x4[7].transform.position.x == 1.85f && squares4x4[7].transform.position.y == 1.07f

                && squares4x4[8].transform.position.x == -1.84f && squares4x4[8].transform.position.y == 0.14f
                && squares4x4[9].transform.position.x == -0.61f && squares4x4[9].transform.position.y == 0.14f
                && squares4x4[10].transform.position.x == 0.62f && squares4x4[10].transform.position.y == 0.14f
                && squares4x4[11].transform.position.x == 1.85f && squares4x4[11].transform.position.y == 0.14f

                && squares4x4[12].transform.position.x == -1.84f && squares4x4[12].transform.position.y == -0.79f
                && squares4x4[13].transform.position.x == -0.61f && squares4x4[13].transform.position.y == -0.79f
                && squares4x4[14].transform.position.x == 0.62f && squares4x4[14].transform.position.y == -0.79f
                && squares4x4[15].transform.position.x == 1.85f && squares4x4[15].transform.position.y == -0.79f)
            {
                // canvas.SetActive(true);
                SettingsData.Win = true;
                SaveBestTime();

                for (int i = 0; i < 16; i++)
                {
                    squares4x4[i].transform.position = stickedPositions4x4[i];
                    //  iTween здесь не работает
                    // iTween.MoveTo(squares[i], iTween.Hash(
                    //     "position", stickedPositions[i],
                    //     "time", .3f
                    // ));
                }

                PlayAudio(4);
                return true;
            }
            else
                return false;
        }
        else if (size == 12)
        {
            if (squares4x3[0].transform.position.x == -1.84f && squares4x3[0].transform.position.y == 1.85f
                && squares4x3[1].transform.position.x == -0.61f && squares4x3[1].transform.position.y == 1.85f
                && squares4x3[2].transform.position.x == 0.62f && squares4x3[2].transform.position.y == 1.85f
                && squares4x3[3].transform.position.x == 1.85f && squares4x3[3].transform.position.y == 1.85f

                && squares4x3[4].transform.position.x == -1.84f && squares4x3[4].transform.position.y == 0.6f
                && squares4x3[5].transform.position.x == -0.61f && squares4x3[5].transform.position.y == 0.6f
                && squares4x3[6].transform.position.x == 0.62f && squares4x3[6].transform.position.y == 0.6f
                && squares4x3[7].transform.position.x == 1.85f && squares4x3[7].transform.position.y == 0.6f

                && squares4x3[8].transform.position.x == -1.84f && squares4x3[8].transform.position.y == -0.65f
                && squares4x3[9].transform.position.x == -0.61f && squares4x3[9].transform.position.y == -0.65f
                && squares4x3[10].transform.position.x == 0.62f && squares4x3[10].transform.position.y == -0.65f
                && squares4x3[11].transform.position.x == 1.85f && squares4x3[11].transform.position.y == -0.65f)
            {
                // canvas.SetActive(true);
                SettingsData.Win = true;
                SaveBestTime();

                for (int i = 0; i < 12; i++)
                {
                    squares4x3[i].transform.position = stickedPositions4x3[i];
                    //  iTween здесь не работает
                    // iTween.MoveTo(squares[i], iTween.Hash(
                    //     "position", stickedPositions[i],
                    //     "time", .3f
                    // ));
                }

                PlayAudio(4);
                return true;
            }
            else
                return false;
        }
        return false;
    }

    IEnumerator PrepareVideo()
    {
        if (size == 9)
            for (int i = 0; i < 9; i++)
                squares3x3[i].GetComponent<SpriteRenderer>().color = Color.black;
        else if (size == 16)
            for (int i = 0; i < 16; i++)
                squares4x4[i].GetComponent<SpriteRenderer>().color = Color.black;
        else if (size == 12)
            for (int i = 0; i < 12; i++)
                squares4x3[i].GetComponent<SpriteRenderer>().color = Color.black;

        for (int i = 0; i < size; i++)
        {
            videoPlayers[i].Prepare();
            while (!videoPlayers[i].isPrepared)
                yield return null;
        }

        if (size == 9)
            for (int i = 0; i < 9; i++)
                squares3x3[i].GetComponent<SpriteRenderer>().color = Color.white;
        else if (size == 16)
            for (int i = 0; i < 16; i++)
                squares4x4[i].GetComponent<SpriteRenderer>().color = Color.white;
        else if (size == 12)
            for (int i = 0; i < 12; i++)
                squares4x3[i].GetComponent<SpriteRenderer>().color = Color.white;

        for (int i = 0; i < size; i++)
            videoPlayers[i].Play();

        loadingText.enabled = false;
        loadingText.gameObject.SetActive(false);
        loadingText.gameObject.layer = 0;
        videoIsReady = true;
    }

    public void PlayAudio(int id)
    {
        if (id == 1 && SettingsData.SfxOn)
            SoundIDDrop = AndroidNativeAudio.play(FileIDDrop);
        else if (id == 2 && SettingsData.SfxOn)
            SoundIDBong = AndroidNativeAudio.play(FileIDBong);
        else if (id == 3)
        {
            ANAMusic.pause(musicID);
            // ANAMusic.release(musicID);
        }
        else if (id == 4)
        {
            ANAMusic.pause(musicID);
            // ANAMusic.release(musicID);

            ANAMusic.play(applauseID);
        }
        else if (id == 5)
        {
            ANAMusic.pause(applauseID);
            // ANAMusic.release(applauseID);

            AndroidNativeAudio.unload(FileIDDrop);
            AndroidNativeAudio.unload(FileIDBong);
            AndroidNativeAudio.releasePool();
        }
    }

    void SaveBestTime()
    {
        int timeInSeconds = hours * 60 * 60 + minutes * 60 + seconds;
        int timeInDecimals = hundreds;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 1)
        {
            if (timeInSeconds < PlayerPrefs.GetInt("level1BestTime"))
            {
                PlayerPrefs.SetInt("level1BestTime", timeInSeconds);
                PlayerPrefs.SetInt("level1BestTimeInDecimals", timeInDecimals);

                bestTimeText.enabled = true;
                bestTimeText.gameObject.SetActive(true);
            }
            else if (timeInSeconds == PlayerPrefs.GetInt("level1BestTime"))
            {
                if (timeInDecimals < PlayerPrefs.GetInt("level1BestTimeInDecimals"))
                {
                    PlayerPrefs.SetInt("level1BestTime", timeInSeconds);
                    PlayerPrefs.SetInt("level1BestTimeInDecimals", timeInDecimals);

                    bestTimeText.enabled = true;
                    bestTimeText.gameObject.SetActive(true);
                }
            }
            PlayerPrefs.SetInt("level1Time", timeInSeconds);
            PlayerPrefs.SetInt("level1TimeInDecimals", timeInDecimals);
        }
        if (sceneIndex == 2)
        {
            if (timeInSeconds < PlayerPrefs.GetInt("level2BestTime"))
            {
                PlayerPrefs.SetInt("level2BestTime", timeInSeconds);
                PlayerPrefs.SetInt("level2BestTimeInDecimals", timeInDecimals);

                bestTimeText.enabled = true;
                bestTimeText.gameObject.SetActive(true);
            }
            else if (timeInSeconds == PlayerPrefs.GetInt("level2BestTime"))
            {
                if (timeInDecimals < PlayerPrefs.GetInt("level2BestTimeInDecimals"))
                {
                    PlayerPrefs.SetInt("level2BestTime", timeInSeconds);
                    PlayerPrefs.SetInt("level2BestTimeInDecimals", timeInDecimals);

                    bestTimeText.enabled = true;
                    bestTimeText.gameObject.SetActive(true);
                }
            }
            PlayerPrefs.SetInt("level2Time", timeInSeconds);
            PlayerPrefs.SetInt("level2TimeInDecimals", timeInDecimals);
        }
        if (sceneIndex == 3)
        {
            if (timeInSeconds < PlayerPrefs.GetInt("level3BestTime"))
            {
                PlayerPrefs.SetInt("level3BestTime", timeInSeconds);
                PlayerPrefs.SetInt("level3BestTimeInDecimals", timeInDecimals);

                bestTimeText.enabled = true;
                bestTimeText.gameObject.SetActive(true);
            }
            else if (timeInSeconds == PlayerPrefs.GetInt("level3BestTime"))
            {
                if (timeInDecimals < PlayerPrefs.GetInt("leve3BestTimeInDecimals"))
                {
                    PlayerPrefs.SetInt("level3BestTime", timeInSeconds);
                    PlayerPrefs.SetInt("level3BestTimeInDecimals", timeInDecimals);

                    bestTimeText.enabled = true;
                    bestTimeText.gameObject.SetActive(true);
                }
            }
            PlayerPrefs.SetInt("level3Time", timeInSeconds);
            PlayerPrefs.SetInt("level3TimeInDecimals", timeInDecimals);
        }
        if (sceneIndex == 4)
        {
            if (timeInSeconds < PlayerPrefs.GetInt("level4BestTime"))
            {
                PlayerPrefs.SetInt("level4BestTime", timeInSeconds);
                PlayerPrefs.SetInt("level4BestTimeInDecimals", timeInDecimals);

                bestTimeText.enabled = true;
                bestTimeText.gameObject.SetActive(true);
            }
            else if (timeInSeconds == PlayerPrefs.GetInt("level4BestTime"))
            {
                if (timeInDecimals < PlayerPrefs.GetInt("level4BestTimeInDecimals"))
                {
                    PlayerPrefs.SetInt("level4BestTime", timeInSeconds);
                    PlayerPrefs.SetInt("level4BestTimeInDecimals", timeInDecimals);

                    bestTimeText.enabled = true;
                    bestTimeText.gameObject.SetActive(true);
                }
            }
            PlayerPrefs.SetInt("level4Time", timeInSeconds);
            PlayerPrefs.SetInt("level4TimeInDecimals", timeInDecimals);
        }
        if (sceneIndex == 5)
        {
            if (timeInSeconds < PlayerPrefs.GetInt("level5BestTime"))
            {
                PlayerPrefs.SetInt("level5BestTime", timeInSeconds);
                PlayerPrefs.SetInt("level5BestTimeInDecimals", timeInDecimals);

                bestTimeText.enabled = true;
                bestTimeText.gameObject.SetActive(true);
            }
            else if (timeInSeconds == PlayerPrefs.GetInt("level5BestTime"))
            {
                if (timeInDecimals < PlayerPrefs.GetInt("level5BestTimeInDecimals"))
                {
                    PlayerPrefs.SetInt("level5BestTime", timeInSeconds);
                    PlayerPrefs.SetInt("level5BestTimeInDecimals", timeInDecimals);

                    bestTimeText.enabled = true;
                    bestTimeText.gameObject.SetActive(true);
                }
            }
            PlayerPrefs.SetInt("level5Time", timeInSeconds);
            PlayerPrefs.SetInt("level5TimeInDecimals", timeInDecimals);

            int totalTimeInDecimals = PlayerPrefs.GetInt("level1TimeInDecimals") + PlayerPrefs.GetInt("level2TimeInDecimals") + PlayerPrefs.GetInt("level3TimeInDecimals") + PlayerPrefs.GetInt("level4TimeInDecimals") + PlayerPrefs.GetInt("level5TimeInDecimals");
            int totalTime = PlayerPrefs.GetInt("level1Time") + PlayerPrefs.GetInt("level2Time") + PlayerPrefs.GetInt("level3Time") + PlayerPrefs.GetInt("level4Time") + PlayerPrefs.GetInt("level5Time");
            if (totalTime < PlayerPrefs.GetInt("totalBestTime"))
            {
                if (totalTimeInDecimals < PlayerPrefs.GetInt("totalBestTimeInDecimals"))
                {
                    PlayerPrefs.SetInt("totalBestTime", totalTime);
                    PlayerPrefs.SetInt("totalBestTimeInDecimals", totalTimeInDecimals);
                    PlayerPrefs.SetInt("highScore", 1);
                }
            }
            else if (totalTime == PlayerPrefs.GetInt("totalBestTime"))
            {
                if (totalTimeInDecimals < PlayerPrefs.GetInt("totalBestTimeInDecimals"))
                {
                    PlayerPrefs.SetInt("totalBestTime", totalTime);
                    PlayerPrefs.SetInt("totalBestTimeInDecimals", totalTimeInDecimals);
                    PlayerPrefs.SetInt("highScore", 1);
                }
            }
            else
                PlayerPrefs.SetInt("highScore", 0);
            PlayerPrefs.SetInt("totalTime", totalTime);
            PlayerPrefs.SetInt("totalTimeInDecimals", totalTimeInDecimals);
        }
    }

    public void UnloadVideo()
    {
        Debug.Log("UnloadVideo");
        for (int i = 0; i < size; i++)
        {
            videoPlayers[i].Stop();
            if (size == 9)
            {
                squares3x3[i].GetComponent<SpriteRenderer>().color = Color.black;
                Resources.UnloadAsset(squares3x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip);
                // Destroy(squares3x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip);
                squares3x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = null;
            }
            else if (size == 12)
            {
                squares4x3[i].GetComponent<SpriteRenderer>().color = Color.black;
                Resources.UnloadAsset(squares4x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip);
                // Destroy(squares4x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip);
                squares4x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = null;
            }
            else if (size == 16)
            {
                squares4x4[i].GetComponent<SpriteRenderer>().color = Color.black;
                Resources.UnloadAsset(squares4x4[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip);
                // Destroy(squares4x4[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip);
                squares4x4[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = null;
            }
            videoPlayers[i] = null;
        }
        
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        // Application.GarbageCollectUnusedAssets();
    }

    // void OnDestroy()
    // {
    //     Debug.Log("OnDestroy");
    // }

    void LoadVideo()
    {
        for (int i = 0; i < size; i++)
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    squares3x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = Resources.Load<UnityEngine.Video.VideoClip>("Video/Dweebs/dweebs01_00" + (i + 1).ToString());
                    break;
                case 2:
                    squares3x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = Resources.Load<UnityEngine.Video.VideoClip>("Video/Caminandes/caminandes01_00" + (i + 1).ToString());
                    break;
                case 3:
                    squares4x3[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = Resources.Load<UnityEngine.Video.VideoClip>("Video/Spring/spring01_0" + (i + 1).ToString("D2"));
                    break;
                case 4:
                    squares4x4[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = Resources.Load<UnityEngine.Video.VideoClip>("Video/Sintel/sintel01_0" + (i + 1).ToString("D2"));
                    break;
                case 5:
                    squares4x4[i].GetComponent<UnityEngine.Video.VideoPlayer>().clip = Resources.Load<UnityEngine.Video.VideoClip>("Video/Bigbuck/bigbuck01_0" + (i + 1).ToString("D2"));
                    break;
            }
        }
    }

    public void ToogleSound()
    {
        if (soundToggle.isOn)
        {
            SettingsData.SoundOn = true;
            // if (ANAMusic.isPlaying(musicID))
            ANAMusic.setVolume(musicID, 0.5f);
            // if (ANAMusic.isPlaying(applauseID))
            ANAMusic.setVolume(applauseID, 0.5f);
        }
        else
        {
            Debug.Log(musicID.ToString());
            SettingsData.SoundOn = false;
            // if (ANAMusic.isPlaying(musicID))
            ANAMusic.setVolume(musicID, 0f);
            // if (ANAMusic.isPlaying(applauseID))
            ANAMusic.setVolume(applauseID, 0f);
        }
    }

    public void ToogleSFX()
    {
        if (sfxToggle.isOn)
            SettingsData.SfxOn = true;
        else
            SettingsData.SfxOn = false;
    }

    public void ToggleAssistant()
    {
        if (assistantToggle.isOn)
            SettingsData.AssistantOn = true;
        else
            SettingsData.AssistantOn = false;
    }

    public void HeartIt(GameObject destinationObject, GameObject sourceObject, Vector2 destinationPos, Vector2 sourcePos)
    {
        if (!assistantToggle.isOn)
            return;

        if (size == 9)
        {
            for (int i = 0; i < size; i++)
            {
                if (sourceObject == squares3x3[i].gameObject || destinationObject == squares3x3[i].gameObject)
                {
                    if (sourceObject == squares3x3[i].gameObject && destinationPos.x == positions3x3[i].x && destinationPos.y == positions3x3[i].y)
                    {
                        heart.Play();
                        break;
                    }
                    else if (destinationObject == squares3x3[i].gameObject && sourcePos.x == positions3x3[i].x && sourcePos.y == positions3x3[i].y)
                    {
                        heart.Play();
                        break;
                    }
                }
            }
        }
        else if (size == 12)
        {
            for (int i = 0; i < size; i++)
            {
                if (sourceObject == squares4x3[i].gameObject || destinationObject == squares4x3[i].gameObject)
                {
                    if (sourceObject == squares4x3[i].gameObject && destinationPos.x == positions4x3[i].x && destinationPos.y == positions4x3[i].y)
                    {
                        heart.Play();
                        break;
                    }
                    else if (destinationObject == squares4x3[i].gameObject && sourcePos.x == positions4x3[i].x && sourcePos.y == positions4x3[i].y)
                    {
                        heart.Play();
                        break;
                    }
                }
            }
        }
        else if (size == 16)
        {
            for (int i = 0; i < size; i++)
            {
                if (sourceObject == squares4x4[i].gameObject || destinationObject == squares4x4[i].gameObject)
                {
                    if (sourceObject == squares4x4[i].gameObject && destinationPos.x == positions4x4[i].x && destinationPos.y == positions4x4[i].y)
                    {
                        heart.Play();
                        break;
                    }
                    else if (destinationObject == squares4x4[i].gameObject && sourcePos.x == positions4x4[i].x && sourcePos.y == positions4x4[i].y)
                    {
                        heart.Play();
                        break;
                    }
                }
            }
        }
    }
}
