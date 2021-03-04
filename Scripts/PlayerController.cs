using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    ParticleSystem ps;
    SpriteRenderer sr;
    AudioSource aud;
    float horizontalInput;

    [SerializeField]
    float speed, angSpeed, particleTime, timer;
    [SerializeField]
    bool alive;

    void Start()
    {
        timer = particleTime;
        alive = true;
        aud = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
            Move();
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                GameControl.RunOver();            
        }
    }
    private void Move()
    {
        if (Input.GetKey("a") || Input.GetKey("d") || Input.GetMouseButton(0))
        {
            if (Input.GetKey("a"))
                horizontalInput = 1;
            else
                horizontalInput = -1;


            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)   //if press on left of centre
                    horizontalInput = 1;
                else
                    horizontalInput = -1;
            }
        }
        else
            horizontalInput = 0;

        rb.velocity = transform.up * speed;
        rb.angularVelocity = angSpeed * horizontalInput;
    }
    void Death(){        
        GameControl.RunOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) {
            sr.enabled = false;
            ps.Play();
            alive = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 2) {
            aud.Play();
            sr.enabled = false;
            ps.Play();
            alive = false;
        }
        else {
            GameControl.IncScore();
        }
    }
}
