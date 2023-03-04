using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
   public GameObject pipePrefab;
    public float spawnDelay = 2.0f;
    public float pipeSpeed = 2.0f;
    public float spawnRange = 2.0f;

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnDelay)
        {
            SpawnPipe();
            timer = 0.0f;
        }

        MovePipes();
    }

    private void SpawnPipe()
    {
        float yPos = Random.Range(-spawnRange, spawnRange);

        GameObject pipe = Instantiate(pipePrefab, transform.position + new Vector3(0.0f, yPos, 0.0f), Quaternion.identity);
    }

    private void MovePipes()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        foreach (GameObject pipe in pipes)
        {
            pipe.transform.position += Vector3.left * pipeSpeed * Time.deltaTime;
        }
    }
}
