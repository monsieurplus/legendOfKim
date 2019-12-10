using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class NpcController : MonoBehaviour
{
    // GameController reference
    private GameController game = null;
    private GameController GetGame()
    {
        if (game == null)
            game = GameController.GetInstance();

        return game;
    }

    public enum NpcAction { None, Talk, Design, Massage };

    private bool reachable = false;
    public NpcAction action;

    public GameObject talkIcon;
    [TextArea(2,10)]
    public string talkConfig;
    public UnityEvent talkCallback;

    public ProgressBarController massageProgress;
    public float massageProgressValue = 0f;

    private float massageTimer = -1f;
    public float massageProgressPerSecond = 1f;
    public float massageProgressPerSecondRandomMin = 0f;
    public float massageProgressPerSecondRandomMax = 1f;

    public bool massaging = false;
    private float massagingStart = -1;
    public float massagingDuration = 1f;
    public float massagePower = 10f;
    public float massagePowerRandomMin = 0f;
    public float massagePowerRandomMax = 0f;
    public UnityEvent massageCallback;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        sr.receiveShadows = true;
        sr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    private void OnTriggerEnter(Collider other) {
        reachable = true;
    }

    private void OnTriggerExit(Collider other) {
        reachable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (action != NpcAction.None && reachable && Input.GetKeyDown("space"))
        {
            if (action == NpcAction.Talk && GetGame().dialog.IsAvailable() && !massaging)
            {
                GetGame().ShowDialog(talkConfig, talkCallback);
            }
            else if (action == NpcAction.Massage && !massaging) {
                StartMassage();
            }
        }

        UpdateInteraction();
        UpdateMassageProgress();
    }

    private void UpdateInteraction() {
        // Depending on the NPC action and reachability
        // We show/hide elements
        if (action == NpcAction.None) {
            HideMassage();
            HideTalk();
        }
        else {
            if (action == NpcAction.Talk) {
                HideMassage();
                if (reachable)
                    ShowTalk();
                else
                    HideTalk();
            }
            else if (action == NpcAction.Massage) {
                HideTalk();
                ShowMassage();
            }
        }
    }

    public void SetActionToTalk() {
        this.action = NpcAction.Talk;
    }
    public void SetActionToMassage() {
        this.action = NpcAction.Massage;
    }

    private void ShowTalk() {
        talkIcon.SetActive(true);
    }
    private void HideTalk() {
        talkIcon.SetActive(false);
    }
    private void ShowMassage() {
        massageProgress.Show();
    }
    private void HideMassage() {
        massageProgress.Hide();
    }

    private void UpdateMassageProgress() {
        if (action != NpcAction.Massage) {
            if (massaging) {
                EndMassage();
            }
            return;
        }
            
        
        // Init the second counter
        if (massageTimer == -1f)
            massageTimer = Time.time;

        // Every second
        else if (Time.time - massageTimer >= 1f) {
            massageTimer = Time.time;
            massageProgressValue += massageProgressPerSecond;
            massageProgressValue += Random.Range(massageProgressPerSecondRandomMin, massageProgressPerSecondRandomMax);
        }

        // Apply changes to the progress bar
        massageProgress.SetPercent(massageProgressValue);

        // Handling the massage timer (time before next massage possible)
        if (massaging && Time.time - massagingStart >= massagingDuration) {
            EndMassage();
        }

        // Callback once 100%
        if (massageProgressValue >= 100f) {
            massageCallback.Invoke();
        }
    }

    private void StartMassage() {
        if (massaging)
            return;

        massaging = true;
        massagingStart = Time.time;
        GetGame().StartMassage();

        // Add massage value to the progress
        massageProgressValue += massagePower;
        massageProgressValue += Random.Range(massagePowerRandomMin, massagePowerRandomMax);
    }

    private void EndMassage() {
        massaging = false;
        massagingStart = -1f;
        GetGame().EndMassage();
    }
}
