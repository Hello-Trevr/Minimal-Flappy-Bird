using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject[] pipePrefab;
    public GameObject playerMover;
    public int numberOfPoints;
    public float spawnDelay = 2.0f;
    public float maxSpawnDelay;
    public float pipeSpeed = 2.0f;
    public float maxPipeSpeed;
    public float spawnRange = 2.0f;
    public float difficultyScale = 2f;
    public int pipeObjectCounter; //Determines the number of Pipe prefabs to began spawning

    private float timer = 0.0f;

    bool isHarder = false;


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnDelay)
        {
            SpawnPipe();
            timer = 0.0f;
        }

        MovePipes();
        CountPoints();
        DifficultyController();
    }

    private void SpawnPipe()
    {
        float yPos = Random.Range(-spawnRange, spawnRange);
        int pipeSort = Random.Range(0, pipeObjectCounter - 1);

        GameObject pipe = Instantiate(pipePrefab[pipeSort], transform.position + new Vector3(0.0f, yPos, 0.0f), Quaternion.identity);

    }

    void DifficultyController()
    {
        if(numberOfPoints % difficultyScale == 0 && numberOfPoints > 0 && !isHarder)
        {
           
            IncrementPipeVariables();

            if(pipeObjectCounter < pipePrefab.Length)
            {
                pipeObjectCounter++;
            }

            isHarder = true;
        } 
        else if(numberOfPoints % 2 != 0)
        {
            isHarder = false;
        }
    }

    void IncrementPipeVariables()
    {
        if(maxPipeSpeed > pipeSpeed)
        {
            pipeSpeed = pipeSpeed + .2f;
        }
        
        if(maxSpawnDelay < spawnDelay)
        {
            spawnDelay = spawnDelay - .2f;
        }
    }

    private void MovePipes()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        foreach (GameObject pipe in pipes)
        {
            pipe.transform.position += Vector3.left * pipeSpeed * Time.deltaTime;
        }
    }

    void CountPoints()
    {
        numberOfPoints = playerMover.GetComponent<PlayerMover>().points;
    }
}
