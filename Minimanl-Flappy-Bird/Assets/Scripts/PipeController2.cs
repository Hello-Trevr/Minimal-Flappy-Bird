using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController2 : MonoBehaviour
{
    public float minY = -1.0f;
    public float maxY = 3.0f;

    public float xPos = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        float yPos = Random.Range(minY, maxY);
        transform.position = new Vector3(xPos, yPos, 0f);
    }

    // Update is called once per frame
    void Update()
    {
          if (transform.position.x < -15.0f)
        {
            Destroy(gameObject);
        }
    }
}
