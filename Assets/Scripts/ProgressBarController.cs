using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject progressBarBackground;
    private SpriteRenderer progressBarRenderer;
    private float maxWidth = 0.27f;
    private Vector2 progressBarSize;

    // Start is called before the first frame update
    void Start()
    {
        progressBarRenderer = progressBar.GetComponent<SpriteRenderer>();
        progressBarSize = progressBarRenderer.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show() {
        progressBar.SetActive(true);
        progressBarBackground.SetActive(true);
    }

    public void Hide() {
        progressBar.SetActive(false);
        progressBarBackground.SetActive(false);
    }

    public void SetPercent(float percent) {
        percent = Mathf.Clamp(percent, 0f, 100f);
        float newWidth = (percent/100f) * maxWidth;
        ApplyWidth(newWidth);
    }

    private void ApplyWidth(float width) {
        progressBar.SetActive(width > 0f);
        progressBarSize.x = width;
        progressBarRenderer.size = progressBarSize;
    }
}
