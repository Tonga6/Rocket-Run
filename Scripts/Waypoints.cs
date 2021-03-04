using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    GameObject sys;
    SpriteRenderer sr;
    Collider2D col;
    [SerializeField]
    float angSpeed, cooldown, timer;
    [SerializeField]
    bool active;
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        sr.enabled = false;
        col.enabled = false;
        timer = cooldown;
    }

    private void Update()
    {   
        if (active)
            transform.Rotate(0, 0, 50 * Time.deltaTime); //rotates 50 degrees per second around z axis
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                active = true;
                sr.enabled = true;
                col.enabled = true;
                timer = cooldown;                
                newWaypoint();
            }
        }
           
    }
    void newWaypoint()
    {
        Vector3 waypoint = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(200, Screen.width-100), Random.Range(200, Screen.height-100), 0));
        waypoint.z = 0;
        transform.position = waypoint;                
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && active)
        {
            aud.Play();
            active = false;
            sr.enabled = false;
            col.enabled = false;
        }
    }
}
