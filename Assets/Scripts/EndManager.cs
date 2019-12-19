using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndManager : MonoBehaviour
{
    public GameObject mechanais;
    public GameObject player; // Playable Kim
    public GameObject actor; // Not playable Kim

    public CameraController cam;
    public GameObject camFocus;

    public DialogController dialog;
    public GameObject endLabel;

    [TextArea(2,50)]
    public string dialogDarchis;

    [TextArea(2,50)]
    public string dialogAnais;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init() {
        mechanais.SetActive(false);
        actor.SetActive(false);
        endLabel.SetActive(false);
    }

    // Darhis talk to Kim
    public void Step1() {
        player.SetActive(false);
        actor.SetActive(true);

        cam.FollowObject(camFocus);

        UnityEvent talkCallback = new UnityEvent();
        talkCallback.AddListener(Step2);
        dialog.SetDialogConfig(dialogDarchis, talkCallback);
        dialog.Show();
    }

    // Anais arrive
    public void Step2() {
        mechanais.SetActive(true);
        GetComponent<Animator>().SetBool("mechanais_coming", true);
    }

    // Anais talk to Kim
    public void Step3() {
        GetComponent<Animator>().SetBool("talking", true);

        UnityEvent talkCallback = new UnityEvent();
        talkCallback.AddListener(Step4);
        dialog.SetDialogConfig(dialogAnais, talkCallback);
        dialog.Show();
    }

    // Envolation
    public void Step4() {
        cam.followSpeed = 10f;
        GetComponent<Animator>().SetBool("takeoff", true);

    }

    // Envolation Idle
    public void Step5() {
        GetComponent<Animator>().SetBool("flying", true);
    }

    public void Step6() {
        endLabel.SetActive(true);
    }


}
