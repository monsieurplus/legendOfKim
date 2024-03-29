﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 startPosition;

    private bool follow = false;
    private GameObject followObject;
    public Vector3 followDelta;
    public float followSpeed = 1.5f;

    public float minX = 0f;
    public float maxX = 10f;

    private bool target = false;
    private float targetX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            // follow the object
            Follow();
        }
        else if (target)
        {
            // something else
        }
    }

    public void FollowObject(GameObject obj)
    {
        follow = true;
        followObject = obj;
    }

    private void Follow()
    {
        Vector3 targetPosition = followObject.transform.position - followDelta;
        Vector3 cameraPosition = this.transform.position;

        Vector3 delta = targetPosition - cameraPosition;
        cameraPosition += delta * Mathf.Clamp01(Time.deltaTime) * followSpeed;

        this.SetPosition(cameraPosition);
    }

    public void SetPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, minX, maxX);
        this.transform.position = position;
    }

    public void SetPosition(float x)
    {
        this.SetPosition(new Vector3(x, startPosition.y, startPosition.z));
    }


}
