using UnityEngine;
using TMPro;

public class FPSCount : MonoBehaviour
{
    public TextMeshProUGUI count;
    private int frameRateIndex;
    private float[] frameDeltaTimeArray;

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }

    private void Update()
    {
        frameDeltaTimeArray[frameRateIndex] = Time.deltaTime;
        frameRateIndex = (frameRateIndex + 1) % frameDeltaTimeArray.Length;

        count.text = "FPS : "+Mathf.RoundToInt(CalculateFPS()).ToString();
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
