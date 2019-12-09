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
    public AudioSource music;
    
    public bool showIntro = true;
    public IntroScreenController intro;
    private UnityEvent introCallback;

    // Start is called before the first frame update
    void Start()
    {
        // Setting the kinfOfASingleton instance
        GameController.instance = this;
        camera.GetComponent<CameraController>().FollowObject(player.gameObject);

        // Set callback to the intro
        player.controllable = false;
        if (showIntro == true) {
            introCallback = new UnityEvent();
            introCallback.AddListener(StartGame);
            intro.Show(introCallback);
        }
        else {
            intro.gameObject.SetActive(false);
            StartGame();
        }

        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame() {
        player.controllable = true;
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
        player.PlayRandomPouic();
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
