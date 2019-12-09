using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntroScreenController : MonoBehaviour
{
    private float showStart = -1f;
    public float showDuration = 2f;

    public float hideDuration = 1f;
    public float hideStep = 10f;
    private float hideStart = -1f;

    private CanvasGroup canvas;
    private UnityEvent introCallback;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showStart > -1f)
            UpdateShow();
        
        if (hideStart > -1f)
            UpdateHide();
    }

    public void Show(UnityEvent callback) {
        canvas.alpha = 1f;
        showStart = Time.time;
        introCallback = callback;
    }

    private void UpdateShow() {
        if (Time.time - showStart >= showDuration) {
            showStart = -1f;
            Hide();
        }
    }

    public void Hide() {
        hideStart = Time.time;
    }

    private void UpdateHide() {
        float progress = (Time.time - hideStart) / hideDuration;
        float alpha = 1f - progress;

        canvas.alpha = Mathf.Clamp01(Mathf.Floor(alpha*hideStep)/hideStep);

        if (alpha <= 0f) {
            introCallback.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
