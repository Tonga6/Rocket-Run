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
            transform.Rotate(0, 0, 50 * Time.deltaTime); 
        else //wait out timer before spawning new waypoint
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = cooldown;                
                ChangeState();
                NewWaypoint();
            }
        }           
    }
    void NewWaypoint()
    {
        Vector3 waypoint = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(200, Screen.width-100), Random.Range(200, Screen.height-100), 0));
        waypoint.z = 0;
        transform.position = waypoint;                
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If waypoint collides with player while active
        if (collision.gameObject.layer == 8 && active)
        {
            aud.Play();
            ChangeState();
        }
    }
    private void ChangeState(){
        active = active;
        sr.enabled = !sr.enabled;
        col.enabled = !col.enabled;
    }
}
