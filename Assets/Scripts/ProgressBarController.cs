using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    public GameObject progressBar;
    private SpriteRenderer progressBarSprite;
    private float maxWidth = 0.27f;

    // Start is called before the first frame update
    void Start()
    {
        progressBarSprite = progressBar.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPercent(float percent) {
        percent = Mathf.Clamp(percent, 0f, 100f);
        float newWidth = (percent/100f) * maxWidth;
    }
}
