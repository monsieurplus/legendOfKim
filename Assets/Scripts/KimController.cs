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

    private AudioSource audioSource;
    public AudioClip[] soundDesign;
    public AudioClip[] soundPouic;
        
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
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

    public void SetTalking(bool status)
    {
        animator.SetBool("talking", status);
    }

    public void SetMassaging(bool status)
    {
        Vector3 pos = this.transform.position;
        pos.z = (status ? 0.365f : -0.147f);
        this.transform.position = pos;

        animator.SetBool("massaging", status);
    }

    public void PlayRandomDesign() {
        int clipIndex = Mathf.FloorToInt(Random.Range(0f, (float)soundDesign.Length));
        if (clipIndex == soundDesign.Length)
            clipIndex--;

        audioSource.PlayOneShot(soundDesign[clipIndex]);
    }

    public void PlayRandomPouic() {
        int clipIndex = Mathf.FloorToInt(Random.Range(0f, (float)soundPouic.Length));
        if (clipIndex == soundPouic.Length)
            clipIndex--;

        audioSource.PlayOneShot(soundPouic[clipIndex]);
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
