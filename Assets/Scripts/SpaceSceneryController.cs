using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSceneryController : MonoBehaviour
{
    private bool scrolling;
    private float scrollSpeed = 0f;
    private float scrollTargetSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (scrolling) {
            UpdateScrollSpeed();
            UpdateScrolling();
        }
    }

    public void StartScrolling(float targetSpeed = 1f) {
        scrolling = true;
        scrollTargetSpeed = targetSpeed;
    }

    public void StopScrolling() {
        scrolling = false;
        scrollSpeed = 0f;
    }

    public bool IsScrolling() {
        return scrolling;
    }

    private void UpdateScrollSpeed() {
        if (scrolling && scrollSpeed != scrollTargetSpeed) {
            float deltaSpeed = scrollTargetSpeed - scrollSpeed;
            if (deltaSpeed < 0.1f)
                scrollSpeed = scrollTargetSpeed;
            else
                scrollSpeed += (scrollTargetSpeed - scrollSpeed) * Time.deltaTime;
        }
    }  

    private void UpdateScrolling() {
        if (scrolling && scrollSpeed != 0f)
            this.transform.Translate(scrollSpeed * Time.deltaTime, 0f, 0f);
    }
}
