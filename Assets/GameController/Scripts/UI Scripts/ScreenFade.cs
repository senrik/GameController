using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour {

    public float fadeTime = 1.0f;
    public Color fadeColor = Color.black;
    public Material fadeMaterial = null;
    public GameObject fadeImg;
    private static bool isFading = true;
    private static Color currentColor, targetColor;
    private Color deltaColor = new Color(0, 0, 0, 0);
    private static float fadeAlpha = 0.0f;
    private CanvasGroup fadeGrp;

    private void Awake()
    {
        currentColor = Color.black;
        fadeMaterial.color = currentColor;
        fadeGrp = fadeImg.GetComponent<CanvasGroup>();
        fadeAlpha = fadeGrp.alpha;
    }

    public static void StartFade(Color target)
    {
        isFading = true;
        targetColor = target;
    }

    void PingPongFade()
    {
        fadeAlpha = Mathf.PingPong(Time.time, 1);
        fadeGrp.alpha = fadeAlpha;
        Debug.Log("fadeAlpha: " + fadeAlpha);
    }
    // Update is called once per frame
    void Update () {

    }

    public static bool ScreenClear
    {
        get { return (fadeAlpha > 0.9f) ? true : false; }
    }
}
