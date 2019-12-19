using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    public float min;
    public float max;

    private bool status = true;
    private float nextChange = -1f;

    // Start is called before the first frame update
    void Start()
    {
        ScheduleNextSwitch();
    }

    private void ScheduleNextSwitch() {
        nextChange = Time.time + Random.Range(min, max);
    }

    private void ChangeStatus() {
        status = !status;

        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = status;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextChange) {
            ChangeStatus();
            ScheduleNextSwitch();
        }
    }
}
