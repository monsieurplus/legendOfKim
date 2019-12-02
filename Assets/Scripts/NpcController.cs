using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public NpcAction action;
    public GameObject actionIcon;
    private bool actionable = false;

    [TextArea(2,10)]
    public string talkConfig;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (actionable && Input.GetKey("space"))
        {
            if (action == NpcAction.Talk && GetGame().dialog.IsAvailable())
            {
                game.ShowDialog("Bobby", talkConfig, this);
            }
        }
    }

    private void SetActionIcon(bool visibility)
    {
        actionable = visibility;
        actionIcon.SetActive(visibility);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<KimController>())
        {
            SetActionIcon(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<KimController>())
        {
            SetActionIcon(false);
        }
    }
}
