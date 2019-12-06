using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    public GameObject startMessage;
    private bool startEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && startEnabled) {
            SceneManager.LoadScene("Level01");
        }
    }

    public void ReadyToStart() {
        startMessage.SetActive(true);
        startEnabled = true;
    }
}
