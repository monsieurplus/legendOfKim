using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    // Kind of a Singleton but not really
    private static GameController instance;
    public static GameController GetInstance()
    {
        return GameController.instance;
    }

    public KimController player;
    public new CameraController camera;
    public DialogController dialog;

    // Start is called before the first frame update
    void Start()
    {
        // Setting the kinfOfASingleton instance
        GameController.instance = this;

        camera.GetComponent<CameraController>().FollowObject(player.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowDialog(string dialogConfig, UnityEvent dialogCallback = null)
    {
        player.controllable = false;
        player.SetTalking(true);
        dialog.SetDialogConfig(dialogConfig, dialogCallback);
        dialog.Show();
    }

    public void HideDialog()
    {
        dialog.Hide();
        player.SetTalking(false);
        player.controllable = true;
    }

    public void StartMassage() {
        player.controllable = false;
        player.SetMassaging(true);
    }

    public void EndMassage() {
        player.controllable = true;
        player.SetMassaging(false);
    }

    // every 2 seconds perform the print()
    private IEnumerator GiveBackControl(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.controllable = true;   
    }
}
