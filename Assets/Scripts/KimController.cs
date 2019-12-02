using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimController : MonoBehaviour
{
    public bool controllable = true;

    private bool moving = false;
    private float movingDirection = 0f;

    public float movingSpeed = 1f;

    private Animator animator;
        
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controllable) {
            bool keyPressedLeft = Input.GetKey("left");
            bool keyPressedRight = Input.GetKey("right");

            if (keyPressedLeft == keyPressedRight)
            {
                moving = false;
                movingDirection = 0f;
            }
            else if (keyPressedLeft)
            {
                moving = true;
                movingDirection = -1f;
            }
            else if (keyPressedRight)
            {
                moving = true;
                movingDirection = 1f;
            }
        }

        UpdatePosition();
        UpdateAnimator();
    }

    private void UpdatePosition()
    {
        if (moving)
            transform.Translate(new Vector3(Time.deltaTime * movingDirection * movingSpeed, 0f, 0f));
    }

    private void UpdateAnimator() {
        animator.SetBool("moving", moving);
        animator.SetFloat("moving_direction", movingDirection);
    }

    public void SetTalking(bool talking)
    {
        animator.SetBool("talking", talking);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Kim enters " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Kim exits " + other.gameObject.name);
    }*/
}
