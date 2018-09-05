using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameController
{
    public class FullscreenCanvas : MonoBehaviour
    {

        public GameObject fadeImg;


        public float fadeTime = 2.0f;
        public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

        //public Shader fadeShader = null;

        //private Material fadeMaterial = null;
        private bool isFading = false;
        private CanvasGroup fadeGrp;

        private void Awake()
        {
            //fadeMaterial = (fadeShader != null) ? new Material(fadeShader) : new Material(Shader.Find("Transparent/Diffuse"));
            fadeGrp = fadeImg.GetComponent<CanvasGroup>();
        }

        // Use this for initialization
        void Start()
        {
            
        }

        private void OnEnable()
        {
            //SceneManager.sceneLoaded += ScreenFade;
        }

        private void OnDisable()
        {
            //SceneManager.sceneLoaded -= ScreenFade;
        }
        /// <summary>
        /// Either fades the screen out to black, or fades the screen in depending on the passed in bool value.
        /// </summary>
        /// <param name="fade">If true the screen will fade in, otherwise the screen will fade to black.</param>
        public void ScreenFade(bool fade)
        {
            if(!isFading)
            {
                isFading = true;
                StartCoroutine(ScreenFadeCor(fade));
            }
        }
        IEnumerator ScreenFadeCor(bool fade)
        {
            float elapsedTime = 0.0f;

            if(!fade)
            {
                if(fadeGrp.alpha >= 1.0f)
                {
                    isFading = false;
                }
            }
            else
            {
                if (fadeGrp.alpha <= 0.0f)
                {
                    isFading = false;
                }
            }

            while(isFading)
            {
                yield return new WaitForEndOfFrame();

                if(!fade)
                {
                    if((elapsedTime / fadeTime) >= 1.0f)
                    {
                        fadeGrp.alpha = 1.0f;
                        isFading = false;
                    }
                    else
                    {
                        fadeGrp.alpha = Mathf.Clamp01(elapsedTime / fadeTime);
                    }
                    
                }
                else
                {
                    if ((elapsedTime / fadeTime) >= 1.0f)
                    {
                        fadeGrp.alpha = 0.0f;
                        isFading = false;
                    }
                    else
                    {
                        fadeGrp.alpha = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
                    }
                }

                elapsedTime += Time.deltaTime;                
            }
            
        }

        //private void ScreenFade(Scene arg0, LoadSceneMode arg1)
        //{
        //    StartCoroutine(FadeIn());
        //}

        //IEnumerator FadeIn()
        //{
        //    float elapsedTime = 0.0f;
        //    Color color = fadeMaterial.color = fadeColor;
        //    isFading = true;
        //    while (elapsedTime < fadeTime)
        //    {
        //        yield return new WaitForEndOfFrame();
        //        elapsedTime += Time.deltaTime;
        //        color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
        //        fadeMaterial.color = color;
        //    }
        //    isFading = false;
        //}

        // Update is called once per frame
        void Update()
        {
            //if (isFading)
            //{
            //    fadeMaterial.SetPass(0);
            //    GL.PushMatrix();
            //    GL.LoadOrtho();
            //    GL.Color(fadeMaterial.color);
            //    GL.Begin(GL.QUADS);
            //    GL.Vertex3(0f, 0f, -12f);
            //    GL.Vertex3(0f, 1f, -12f);
            //    GL.Vertex3(1f, 1f, -12f);
            //    GL.Vertex3(1f, 0f, -12f);
            //    GL.End();
            //    GL.PopMatrix();
            //}
        }

        public bool ScreenClear
        {
            get { return (fadeGrp.alpha > 0.9f) ? false : true; }
        }
    }
}

