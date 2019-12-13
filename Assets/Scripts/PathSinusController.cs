using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSinusController : MonoBehaviour
{
    public bool moving = true;

    private Vector3 startPosition;

    public float xSpeed;
    public float xMin;
    public float xMax;
    public float xDelay;

    public float zSpeed;
    public float zMin;
    public float zMax;
    public float zDelay;

    private float yInit;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.localPosition;
        yInit = this.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            UpdateMovement();
    }

    private void UpdateMovement() {
        Vector3 position = Vector3.zero;
        position.y = startPosition.y;

        if (xSpeed == 0f)
            position.x = startPosition.x;
        else
            position.x = startPosition.x + (xMin + (Mathf.Sin(Time.time * xSpeed + xDelay)+1f)/2f * (xMax - xMin));

        if (zSpeed == 0f)
            position.z = startPosition.z;
        else
            position.z = startPosition.z + (zMin + Mathf.Sin(Time.time * zSpeed + zDelay)+1f)/2f * (zMax - zMin);

        this.transform.localPosition = position;
    }
}
