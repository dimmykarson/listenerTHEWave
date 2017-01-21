using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Star : MonoBehaviour {

    protected bool on;
    protected Image image;
    protected float lowAlpha = 0.3f;
    public void TurnOff()
    {
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = lowAlpha;
        image.color = c;
    }
    public void TurnOn()
    {
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = 1;
        image.color = c;
    }
}
