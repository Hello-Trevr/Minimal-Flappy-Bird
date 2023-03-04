using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMover : MonoBehaviour
{

    public float jumpForce = 5.0f;

    float jumpForceCache;
    public float gravity = 10.0f;
    float gravityCache;
    public float maxFallSpeed = -20.0f;

    public int points;
    public TextMeshProUGUI score;

    private Rigidbody2D rb2d;
    public bool isDead = false;

    public GameObject pipeSpawner;

    public AudioClip successSFX;
    public AudioClip failSFX;
    public AudioClip jumpSFX;

    AudioSource playerSounds;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSounds = GetComponent<AudioSource>();
        StartSequence();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space)  && !isDead)
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isDead)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            Playing();
        }

        // if(Input.GetKeyDown(KeyCode.Space) && isDead)
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isDead)
        {
            ReloadScene();
        }

        if (rb2d.velocity.y < maxFallSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxFallSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rb2d.velocity += new Vector2(0, -gravity * Time.deltaTime);
        }
    }

    void StartSequence()
    {
        jumpForceCache = jumpForce;
        gravityCache = gravity;
        pipeSpawner.GetComponent<PipeSpawner>().enabled = false;
        rb2d.gravityScale = 0;
        jumpForce = 0;
        gravity = 0;
        score.text = "0";
    }

    void Playing()
    {
        jumpForce = jumpForceCache;
        gravity = gravityCache;
        pipeSpawner.GetComponent<PipeSpawner>().enabled = true;
        rb2d.gravityScale = 1;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            isDead = true;
            // Game over logic here
            Debug.Log("Hit");

            FailSequence();
            
        }
    }

    void FailSequence()
    {
        pipeSpawner.GetComponent<PipeSpawner>().enabled = false;
        rb2d.gravityScale = 0;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll; 
        jumpForce = 0;
        gravity = 0;
        playerSounds.PlayOneShot(failSFX);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if(other.gameObject.tag == "PointCollider" && !isDead)
        {
            points++;
            Debug.Log(points);
            score.text = points.ToString();
            playerSounds.PlayOneShot(successSFX);
        }
    }
}
