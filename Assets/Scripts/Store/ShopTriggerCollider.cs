using UnityEngine;

public class ShopTriggerCollider : Interact
{

    public GameObject House1;


    void Start()
    {
        radius = 4;
        UI_Shop.test.Hide();
    }

    public override void Interacting()
    {
        Time.timeScale = 0;
        Camera.main.GetComponent<CameraController>().paused = true;
        showCursor(true);
        UI_Shop.test.Show();


    }

    public override void CloseInteract()
    {
        UI_Shop.test.Hide();
        Time.timeScale = 1;
        Camera.main.GetComponent<CameraController>().paused = false;
        showCursor(false);
        //UI_Shop.test.Hide();


    }





    private void showCursor(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = b;
    }

}
