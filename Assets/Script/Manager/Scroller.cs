using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    //loop image variable
    [SerializeField] private RawImage img;
    static public float speed = 0.15f;

    //color change variable
    [SerializeField] [Range(0f,1f)]float lerpTime;
    [SerializeField] Color[] colors;
    int colorIndex = 0;
    int colorsLength { get { return colors.Length; }}
    float t = 0;

    private void FixedUpdate()
    {
        //code s'occupent de loop l'image du background
        img.uvRect = new Rect(img.uvRect.position+new Vector2(speed, 0) *Time.fixedDeltaTime, img.uvRect.size);

        //code permettant de changer la couleur du temps en fonction du temps et suivant l'interpolation lin?aire
        img.color = Color.Lerp(img.color, colors[colorIndex], lerpTime*Time.fixedDeltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime*Time.fixedDeltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= colorsLength) ? 0 : colorIndex;
        }
        
    }
}
