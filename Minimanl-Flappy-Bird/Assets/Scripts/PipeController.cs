using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public float minY = -1.0f;
    public float maxY = 3.0f;
    public float xPos = 10.0f;
    public float minGap = 2.0f;
    public float maxGap = 4.0f;

    private void Start()
    {
        float yPos = Random.Range(minY, maxY);
        transform.position = new Vector3(xPos, yPos, 0.0f);

        float gapSize = Random.Range(minGap, maxGap);

        Transform topPipe = transform.Find("TopPipe");
        Transform bottomPipe = transform.Find("BottomPipe");

        topPipe.position = new Vector3(topPipe.position.x, yPos + gapSize / 2.0f, topPipe.position.z);
        bottomPipe.position = new Vector3(bottomPipe.position.x, yPos - gapSize / 2.0f, bottomPipe.position.z);
    }

    private void Update()
    {
        if (transform.position.x < -15.0f)
        {
            Destroy(gameObject);
        }
    }
}
