using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogItem
{
    public string name;
    public string text;
}

public class DialogController : MonoBehaviour
{
    private bool isVisible = false;
    private bool isAvailable = true;

    public GameObject dialogContainer;
    public TMP_Text dialogText;
    public GameObject dialogNameContainer;
    public TMP_Text dialogNameText;
    public GameObject dialogNext;

    private CanvasGroup canvasGroup;

    private DialogItem[] dialogs = new DialogItem[20];
    private int dialogsLength = 0;
    private int currentDialog = 0;

    private UnityEvent callback;


    private bool characterShowing = false;
    private int characterNumber = 0;
    private float characterLastTime;
    public float characterWriteDuration = 0.1f;

    private float nextLastTime;
    private bool nextVisible = false;
    public float nextBlinkDuration = 0.1f;

    private bool spaceReleased = false;

    // Start is called before the first frame update
    void Start()
    {
        // Init a bunch of empty dialog lines
        for (int i = 0; i < 20; i++)
            dialogs[i] = new DialogItem();

        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && spaceReleased)
        {
            spaceReleased = false;

            // Actions on Space press
            if (isVisible)
            {
                if (characterShowing)
                {
                    FinishCharacterShowing();
                }
                else
                {
                    NextDialog();
                }
            }
        }

        if (!Input.GetKeyDown("space"))
        {
            spaceReleased = true;

            // When the dialog box gets hidden, we wait that the space key is released to make the dialog box available again
            // This avoid being stuck in a dialog box loop
            if (!isVisible && !isAvailable)
                isAvailable = true;
        }

        // Updates
        if (characterShowing)
            UpdateCharacterShowing();

        if (isVisible)
            UpdateNextButton();
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void SetDialogConfig(string dialogConfig, UnityEvent dialogCallback)
    {
        callback = null;
        if (dialogCallback != null)
            callback = dialogCallback;

        char[] configSep = {'_'};
        string[] configs = dialogConfig.Split(configSep);

        char[] configPartSep = {'\n'};
        char[] trimChars = {' ', '\n'};
        string[] configParts;

        dialogsLength = configs.Length;
        for (int i=0; i < configs.Length; i++) {
            Debug.Log(configs[i]);
            configParts = configs[i].Trim(trimChars).Split(configPartSep);
            
            if (configParts.Length == 1) {
                dialogs[i].name = "";
                dialogs[i].text = configParts[0];
            }
            else {
                dialogs[i].name = configParts[0];
                dialogs[i].text = configParts[1];
            }
        }
    }

    private void SetCurrentDialog(int dialogIndex)
    {
        currentDialog = dialogIndex;
        characterShowing = true;
        characterLastTime = Time.time;
        characterNumber = 0;

        SetName(dialogs[currentDialog].name);
        SetText("");

        nextLastTime = Time.time;
    }

    public void Show()
    {
        isVisible = true;
        isAvailable = false;
        spaceReleased = false;

        SetCurrentDialog(0);
        canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        isVisible = false;
        isAvailable = false;

        currentDialog = 0;
        canvasGroup.alpha = 0f;
    }

    private void SetName(string name)
    {
        dialogNameContainer.GetComponent<CanvasGroup>().alpha = (name == "" ? 0f : 1f);
        dialogNameText.text = name;
    }
    private void SetText(string text)
    {
        dialogText.text = text;
    }

    private void UpdateCharacterShowing()
    {
        if (Time.time > characterLastTime + characterWriteDuration)
        {
            characterNumber++;
            characterLastTime = Time.time;
        }

        DialogItem dialog = dialogs[currentDialog];
        dialogText.text = dialog.text.Substring(0, characterNumber);

        // Force name update because of contentSizeFitter bugs...
        dialogNameText.text = dialog.name + (characterNumber % 2 == 0 ? " " : "");

        if (characterNumber >= dialog.text.Length)
            characterShowing = false;
    }

    private void FinishCharacterShowing()
    {
        DialogItem dialog = dialogs[currentDialog];
        dialogText.text = dialog.text;
        characterShowing = false;
    }

    private void UpdateNextButton()
    {
        if (currentDialog+1 < dialogsLength && !characterShowing)
        {
            if (Time.time > (nextLastTime + nextBlinkDuration))
            {
                nextVisible = !nextVisible;
                dialogNext.SetActive(nextVisible);

                nextLastTime = Time.time;
            }
        }
        else
        {
            nextVisible = false;
            dialogNext.SetActive(false);
        }
    }

    private void NextDialog()
    {
        if (currentDialog+1 < dialogsLength)
        {
            SetCurrentDialog(currentDialog + 1);
        }
        else
        {
            if (callback != null)
                callback.Invoke();
            
            GameController.GetInstance().HideDialog();
        }
    }
}
