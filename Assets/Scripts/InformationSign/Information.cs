using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : Interact
{
    private GameObject panel;
    private GameObject playerGameObject;
    private Vector3 posShip = new Vector3(38, 7, 26);
    private Vector3 posShop = new Vector3(42, 5.1f, -28);




    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        radius = 4;
        panel = GameObject.Find("Information");
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


    //


    private void showCursor(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = b;
    }

    public void ToMiniGame()
    {
        CloseInteract();
        playerGameObject.transform.position = posShip;
        playerGameObject.transform.Rotate (new Vector3 (0, 90, 0)); 
    }

    public void ToShop()
    {
        CloseInteract();
        playerGameObject.transform.position = posShop;
    }
    //

}
