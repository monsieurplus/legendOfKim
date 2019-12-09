using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGameController : MonoBehaviour
{
    public SpaceSceneryController scenery;
    public SpaceKimController player;
    public GameObject camera;

    public bool scrollOnStart = false;

    public GameObject[] waves;

    // Start is called before the first frame update
    void Start()
    {
        if (scrollOnStart) {
            scenery.StartScrolling();
        }
        
        player.StartWalking();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
