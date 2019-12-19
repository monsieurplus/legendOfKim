using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public string triggeringObjectName;
    public UnityEvent callback;
    public bool multiple;

    private bool called = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == triggeringObjectName) {
            if (!called || multiple)
                callback.Invoke();
            called = true;
        }
    }
}
