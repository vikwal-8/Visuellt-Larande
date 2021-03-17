using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class GameManagerMainWorld : MonoBehaviour
{
    private GameObject hud, pauseScreen, loadCanvas;
    private List<GameObject> gameObjectsList = new List<GameObject>();
    private bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        //loadCanvas = GameObject.Find("CanvasLoadingScreen");
        //AddAndDisable(loadCanvas);
        hud = GameObject.Find("HUD");
        pauseScreen = GameObject.Find("PauseScreen");
        SaveSystem.LoadSave(); //Initilize Save and create if non-existing.
        StartGame();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isGameActive)
                PauseGame();
            else
                StartGame();
        }
    }

    // Start/Continue game
    public void StartGame()
    {
        UpdateDisplayedCoins();
        PlaceBuyItems();
        Time.timeScale = 1;
        Camera.main.GetComponent<CameraController>().paused = false;
        showCursor(false);
        isGameActive = true;
        pauseScreen.SetActive(false);
    }

    // Pause game
    public void PauseGame()
    {
        Time.timeScale = 0;
        Camera.main.GetComponent<CameraController>().paused = true;
        showCursor(true);
        isGameActive = false;
        pauseScreen.SetActive(true);
    }

    // Exit game completely
    public void ExitGame()
    {
        Application.Quit();
    }

    // Show/hide cursor based on boolean
    private void showCursor(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = b;
    }

    private void AddAndDisable(GameObject obj)
    {
        obj.SetActive(false);
        gameObjectsList.Add(obj);
    }
    public GameObject GetObjectByName(string name)
    {
        foreach (GameObject obj in gameObjectsList)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    public static void UpdateDisplayedCoins()
    {
        GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>().text = SaveSystem.LoadSave().credits.ToString();
    }


    public static void PlaceBuyItems()
    {
        SaveData temp = SaveSystem.LoadSave();
        GameObject buyItemsObject = GameObject.Find("BuyableObjects");


        foreach (Transform item in buyItemsObject.transform.Cast<Transform>().ToList())
        {
            if (temp.enabledObjects.Contains(item.gameObject.tag))
            {   
                item.gameObject.SetActive(true);
            }
            else
            {
                
                item.gameObject.SetActive(false);
            }
        }

    }

}
