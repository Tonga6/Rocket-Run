using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontalInput;
    GameObject player;
    ParticleSystem ps;
    SpriteRenderer sr;
    Camera cam;
    [SerializeField]
    float angleToMove, timer, particleTime, shakeDuration;
    [SerializeField]
    bool deathLoop = false;

    [SerializeField]
    GameObject spawnerManager;
    AudioSource aud;

    public float speed, angSpeed;
    void Awake()
    {
        aud = GetComponent<AudioSource>();
        cam = Camera.main;
        spawnerManager = GameObject.FindGameObjectWithTag("Manager");
        timer = particleTime;
        ps = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        sr.enabled = true;
        deathLoop = false;
        timer = particleTime;
    }

    void Update()
    {
        if (deathLoop) {
            sr.enabled = false;
            timer -= Time.deltaTime;
            if (timer < 0)
                spawnerManager.GetComponent<EnemyPool>().addToEnemyQueue(this.gameObject);
        }
        else
            Move();
        
    }
    private void Move() {
        var dir = player.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,angle) , Time.deltaTime * angSpeed);
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer != 2 && collision.gameObject.layer != 5) 
        {
            aud.Play();
            ps.Play();
            deathLoop = true;
            Camera.main.GetComponent<CamShake>().Shake();
        }
    }
}
