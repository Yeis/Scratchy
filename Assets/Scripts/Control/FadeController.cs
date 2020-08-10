using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image blackFade;
    public Text title;
    public Text author;

    // Start is called before the first frame update
    void Start()
    {
        blackFade.canvasRenderer.SetAlpha(0.0f);
        // FadeIn(3);
    }

    public void FadeIn(float seconds)
    {
        blackFade.CrossFadeAlpha(1, seconds, true);
    }

    public void FadeOut(float seconds)
    {
        blackFade.CrossFadeAlpha(0, seconds, true);
    }

}
