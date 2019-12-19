using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SpaceGameController : MonoBehaviour
{
    // Kind of a Singleton but not really
    private static SpaceGameController instance;
    public static SpaceGameController GetInstance()
    {
        return SpaceGameController.instance;
    }

    public SpaceSceneryController scenery;
    public SpaceKimController player;
    public DialogController dialog;
    public AudioSource music;

    public bool showIntro = true;
    public IntroScreenController intro;
    private UnityEvent introCallback;

    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        SpaceGameController.instance = this;

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

    private void StartGame() {
        player.controllable = true;
        player.StartWalking();
        scenery.StartScrolling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel() {
        SceneManager.LoadScene(nextSceneName);
    }
}
