using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    public float acceleration = 1f;
    private Rigidbody rb;

    private float lifeStart;
    public float lifeDuration = 3f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Time.deltaTime * acceleration, 0f, 0f, ForceMode.Acceleration);

        if (Time.time - lifeStart > lifeDuration)
            Destroy(this.gameObject);
    }
}
