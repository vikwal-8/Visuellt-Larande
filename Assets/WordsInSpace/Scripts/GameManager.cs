using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private GameObject startScreen, pauseScreen, gameOverScreen, mainCanvas, wordPlayCanvas, respawnCount, centerArrow;
    public bool isGameActive, outOfBounds, lastBoss;
    public List<char> letterList = new List<char>();
    public AudioClip gameOverAudio;
    private AudioSource audioSource;
    private readonly float worldSize = 100f;
    private List<GameObject> objectsImported = new List<GameObject>();
    private string[] objectsToImport = new string[] { "TitleScreen", "PauseScreen", "GameOverScreen", "MainCanvas", "WordPlayCanvas", "RespawnCount", "CenterArrow", "Player" };
    private int level = 1;


    // Start is called before the first frame update
    void Start()
    {
        FindObjectsFromList();
        Time.timeScale = 0;
        startScreen = FindInActiveObjectByTag("TitleScreen");
        pauseScreen = FindInActiveObjectByTag("PauseScreen");
        gameOverScreen = FindInActiveObjectByTag("GameOverScreen");
        mainCanvas = FindInActiveObjectByTag("MainCanvas");
        wordPlayCanvas = FindInActiveObjectByTag("WordPlayCanvas");
        respawnCount = FindInActiveObjectByTag("RespawnCount");
        centerArrow = FindInActiveObjectByTag("CenterArrow");
        audioSource = GetComponent<AudioSource>();
        lastBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        showCursor(false);
        Time.timeScale = 1;
        startScreen.SetActive(false);
        isGameActive = true;
    }

    public void GameOver()
    {
        showCursor(true);
        Time.timeScale = 0;
        isGameActive = false;
        centerArrow.SetActive(false);
        respawnCount.SetActive(false);
        gameOverScreen.SetActive(true);
        if (letterList.Count == 0)
        {
            gameOverScreen.transform.Find("WordPlayStart").gameObject.SetActive(false);
        }
        audioSource.PlayOneShot(gameOverAudio);
    }

    public void PauseGame()
    {
        if (isGameActive)
        {
            showCursor(true);
            Time.timeScale = 0;
            centerArrow.SetActive(false);
            respawnCount.SetActive(false);
            pauseScreen.SetActive(true);
            isGameActive = false;
        }
    }

    public void ContinueGame()
    {
        showCursor(false);
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        isGameActive = true;
        if (outOfBounds)
        {
            centerArrow.SetActive(true);
            respawnCount.SetActive(true);
        }
    }

    public void RestartGame()
    {
        showCursor(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RestartGame(int pointsToAdd)
    {
        pointsToAdd = (int)(100 + pointsToAdd);
        SaveSystem.AddCredits(pointsToAdd);
        showCursor(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    { 
        Minigame.LoadMainWorld();
    }
    public void Exit(int pointsToAdd)
    {
        pointsToAdd = (int)(100 + pointsToAdd);
        SaveSystem.AddCredits(pointsToAdd);
        Minigame.LoadMainWorld();
    }


    public void StartWordPlay()
    {
        showCursor(true);
        if (letterList.Count >= 0)
        {
            mainCanvas.SetActive(false);
            wordPlayCanvas.SetActive(true);
            wordPlayCanvas.GetComponent<WordPlay>().PlaceAllLetters();
        }
        else
        {
            RestartGame();
        }
    }

    public void AddToArray(char letter)
    {
        letterList.Add(letter);
    }

    public float GetWorldSize()
    {
        return worldSize;
    }

    public void OutOfBoundsScreen(bool param)
    {
        if (param)
        {
            outOfBounds = true;
            centerArrow.SetActive(true);
            respawnCount.SetActive(true);
        }
        else
        {
            outOfBounds = false;
            centerArrow.SetActive(false);
            respawnCount.SetActive(false);
        }

    }

    // Finds inactive gameobjects
    public GameObject FindInActiveObjectByTag(string tag)
    {
        foreach (GameObject obj in objectsImported)
        {
                if (obj.CompareTag(tag))
                {
                    return obj;
                }
        }
        return null;
    }

    public void FindObjectsFromList()
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objectsToImport.Contains(objs[i].tag))
                {
                    objectsImported.Add(objs[i].gameObject);
                }
            }
        }
    }

    public void SetLastBoss()
    {
        lastBoss = true;
    }
    public bool GetLastBoss()
    {
        return lastBoss;
    }

    public void IncreaseLevel()
    {
        level++;
    }
    public int GetLevel()
    {
        return level;
    }

    public void showCursor(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.None : CursorLockMode.Locked; // locks/unlocks cursor to/from center
        Cursor.visible = b;                                                 // hides/shows cursor
    }
}
