using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Minigame : MonoBehaviour
{
    GameObject loadCanvas, startMiniGameCanvas;
    GameManagerMainWorld gameManager;
    Rigidbody player;
    bool created = false;
    private Text progressText;
    bool insideTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManagerMainWorld").GetComponent<GameManagerMainWorld>();
        //loadCanvas = gameManager.GetObjectByName("CanvasLoadingScreen");
        loadCanvas = GameObject.Find("CanvasLoadingScreen");
        loadCanvas.SetActive(false);
        startMiniGameCanvas = GameObject.Find("CanvasStartMiniGame");
        startMiniGameCanvas.SetActive(false);

        if (!created)
        {
            DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && insideTrigger)
        {
            startMiniGameCanvas.SetActive(false);
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }

    public abstract string sceneName
    {
        get;
    }

    private void OnTriggerEnter(Collider collider)
    {
        insideTrigger = true;
        player = collider.gameObject.GetComponent<Rigidbody>();
        startMiniGameCanvas.SetActive(true);
    }


    private void OnTriggerExit()
    {
        insideTrigger = false;
        startMiniGameCanvas.SetActive(false);
    }


    public static void LoadMainWorld()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainWorld");
    }

    IEnumerator LoadSceneAsync(string SceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);
        loadCanvas.SetActive(true);
        Slider slider = loadCanvas.transform.GetChild(0).GetComponent<Slider>();
        progressText = loadCanvas.transform.GetChild(1).GetComponent<Text>();
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = "Laddar Spel: " + progress * 100f + "%";
            yield return null;
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
