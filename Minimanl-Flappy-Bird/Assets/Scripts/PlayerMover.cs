using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Scoring;

public class PlayerMover : MonoBehaviour
{

    public float jumpForce = 5.0f;

    float jumpForceCache;
    public float gravity = 10.0f;
    float gravityCache;
    public float maxFallSpeed = -20.0f;

    public int points;

    [SerializeField] int highScorePoints;
    [SerializeField] int totalScorePoints;
    public TextMeshProUGUI score;

    public TextMeshProUGUI highScore;

    public TextMeshProUGUI currentScore;

    public TextMeshProUGUI totalScore;

    public TextMeshProUGUI newRecordText;

    public Canvas highScoreCanvas;

    private Rigidbody2D rb2d;
    public bool isDead = false;

    public GameObject pipeSpawner;

    public AudioClip successSFX;
    public AudioClip newHighScoreSFX;
    public AudioClip failSFX;
    public AudioClip jumpSFX;

    AudioSource playerSounds;

    public ParticleSystem newHighScoreFX;

    
    void Awake()
    {
        Scoring.Scoring.highScorePoints = PlayerPrefs.GetInt("High Score");
        Scoring.Scoring.totalScorePoints = PlayerPrefs.GetInt("Total Points");
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSounds = GetComponent<AudioSource>();
        StartSequence();

        Application.targetFrameRate = 300;
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

        highScoreCanvas.GetComponent<Canvas>().enabled = false;
        newRecordText.enabled = false;
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

        HighScorePanelController();
        AddToTotalPoints();
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
            score.text = points.ToString();
            playerSounds.PlayOneShot(successSFX);
        }
    }

    void HighScorePanelController()
    {
        highScoreCanvas.GetComponent<Canvas>().enabled = true;
        currentScore.text = points.ToString();

        if(points > Scoring.Scoring.highScorePoints)
        {
           Scoring.Scoring.highScorePoints = points;
           PlayerPrefs.SetInt("High Score", Scoring.Scoring.highScorePoints);
           newHighScoreFX.Play();
           playerSounds.PlayOneShot(newHighScoreSFX);
           newRecordText.enabled = true;
        }

        highScore.text = Scoring.Scoring.highScorePoints.ToString();
    }

    void AddToTotalPoints()
    {
        totalScorePoints = Scoring.Scoring.totalScorePoints + points;
        Scoring.Scoring.totalScorePoints = totalScorePoints;
        totalScore.text = totalScorePoints.ToString();
        PlayerPrefs.SetInt("Total Points", Scoring.Scoring.totalScorePoints);
    }
}

