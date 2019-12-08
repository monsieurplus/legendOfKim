using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelderController : MonoBehaviour
{
    public int lives = 3;
    private bool dead = false;

    private bool hit = false;
    private float hitTime = -1f;
    public float hitDuration = 0.1f;
    public Color hitColor = Color.red;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) {
            if (Time.time - hitTime > hitDuration) { 
               Unhit();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!dead && other.gameObject.name.Substring(0, 5) == "Apple") {
            Hit();
        }
    }

    private void Hit() {
        // invincibility "frame"
        if (hit)
            return;
        
        lives--;
        if (lives <= 0) {
            Die();
            return;
        }

        hit = true;
        hitTime = Time.time;
        sprite.color = hitColor;
    }

    private void Unhit() {
        hit = false;
        hitTime = -1f;
        sprite.color = Color.white;
    }

    private void Die() {
        dead = true;
        GetComponent<PathSinusController>().moving = false;
        GetComponent<Animator>().SetBool("dead", true);
    }

    private void OnTriggerExit(Collider other) {
    }
}
