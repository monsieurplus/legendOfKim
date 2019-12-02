using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextEffect : MonoBehaviour
{
    private CanvasGroup canvas;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.alpha = Mathf.Abs(Mathf.Cos(Time.time * speed));
    }
}
