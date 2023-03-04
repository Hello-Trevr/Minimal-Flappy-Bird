using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{

    
    public GameObject playerMover;
    public GameObject pipeSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        
        playerMover.GetComponent<PlayerMover>().enabled = false;
        pipeSpawner.GetComponent<PipeSpawner>().enabled = false;
        playerMover.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerMover.GetComponent<PlayerMover>().enabled = true;
            pipeSpawner.GetComponent<PipeSpawner>().enabled = true;
            playerMover.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

    }

    
}
