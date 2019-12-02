using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ShowDialog(string name, string text, NpcController npc = null)
    {
        player.controllable = false;
        player.SetTalking(true);
        dialog.SetDialogConfig(name, text);
        dialog.Show();
    }

    public void HideDialog()
    {
        dialog.Hide();
        player.SetTalking(false);
        player.controllable = true;
    }

    // every 2 seconds perform the print()
    private IEnumerator GiveBackControl(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.controllable = true;   
    }
}
