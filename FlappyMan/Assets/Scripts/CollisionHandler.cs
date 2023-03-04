using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public float waitTime;
    bool isTrans;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartFailSequence();
    }

    void StartFailSequence()
    {
        isTrans = true; 
    }
}
