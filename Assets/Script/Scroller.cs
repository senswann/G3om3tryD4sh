using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    //loop image variable
    [SerializeField] private RawImage img;
    private float speed = 0.15f;

    //color change variable
    [SerializeField] [Range(0f,1f)]float lerpTime;
    [SerializeField] Color[] colors;
    int colorIndex = 0;
    int colorsLength { get { return colors.Length; }}
    float t = 0;
    [SerializeField] private bool isPlatform;

    void Update()
    {
        //loop image
        img.uvRect = new Rect(img.uvRect.position+new Vector2(speed, 0) *Time.deltaTime, img.uvRect.size);

        //color change
        if(!isPlatform)
            img.color = Color.Lerp(img.color, colors[colorIndex], lerpTime*Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime*Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= colorsLength) ? 0 : colorIndex;
        }
    }
}
