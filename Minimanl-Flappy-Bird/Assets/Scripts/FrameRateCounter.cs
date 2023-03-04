using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;

    public TextMeshProUGUI frameUI;

    private void Awake() 
    {
        frameDeltaTimeArray = new float[50];
    }

    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

        frameUI.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    private float CalculateFPS()
    {
        float total = 0f;

        foreach(float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }

        return frameDeltaTimeArray.Length / total;
    }
}
