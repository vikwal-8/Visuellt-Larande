using UnityEngine;

public class AchivmentOrb : Interact
{

    private GameObject panel;
    
    


    void Start()
    {
        radius = 4;
        panel = GameObject.Find("Achivment");
        panel.SetActive(false);
    }

    public override void Interacting()
    {
        Time.timeScale = 0;
        Camera.main.GetComponent<CameraController>().paused = true;
        showCursor(true);
        panel.SetActive(true);
    }

    public override void CloseInteract()
    {
        Time.timeScale = 1;
        Camera.main.GetComponent<CameraController>().paused = false;
        showCursor(false);
        panel.SetActive(false);
    }





    private void showCursor(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = b;
    }

}
