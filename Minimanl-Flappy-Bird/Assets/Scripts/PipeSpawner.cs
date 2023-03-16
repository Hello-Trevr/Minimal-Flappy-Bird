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
    public float spawnDelayDecrease;
    public float pipeSpeed = 2.0f;
    public float maxPipeSpeed;
    public float pipeSpeedIncrease;
    public float spawnRange = 2.0f;
    public float difficultySpeedScale = 2f;
    public float difficultyPipeScale = 5f;
    public int pipeObjectCounter; //Determines the number of Pipe prefabs to began spawning

    private float timer = 0.0f;

    bool isHarder = false;
    bool isAddPipe = false;


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
        float randomNumber= Mathf.Round(Random.Range(0, pipeObjectCounter));
        
        int randomPipe = (int) randomNumber;

        GameObject pipe = Instantiate(pipePrefab[randomPipe], transform.position + new Vector3(0.0f, yPos, 0.0f), Quaternion.identity);

    }

    void DifficultyController()
    {
        if(numberOfPoints % difficultySpeedScale == 0 && numberOfPoints > 0 && !isHarder)
        {
           
            IncrementPipeVariables();

            isHarder = true;
        } 
        else if(numberOfPoints % difficultySpeedScale != 0)
        {
            isHarder = false;
        }

        if(numberOfPoints % difficultyPipeScale == 0 && numberOfPoints > 0 && !isAddPipe)
        {
            if(pipeObjectCounter < pipePrefab.Length)
            {
                pipeObjectCounter++;
            }

            isAddPipe = true;
        }
         else if(numberOfPoints % difficultyPipeScale != 0)
        {
            isAddPipe = false;
        }
    }

    void IncrementPipeVariables()
    {
        if(maxPipeSpeed > pipeSpeed)
        {
            pipeSpeed = pipeSpeed + pipeSpeedIncrease;
        }
        
        if(maxSpawnDelay < spawnDelay)
        {
            spawnDelay = spawnDelay - spawnDelayDecrease;
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
