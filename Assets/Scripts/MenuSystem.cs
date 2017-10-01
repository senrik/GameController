using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameController
{
    public class MenuSystem : MonoBehaviour
    {

        public FullscreenCanvas fullCanvas;
        //public GameObject mapCanvas;
        //public CanvasGroup debugCanvasGroup;
        //public Text debugText;
        public PlayerRig player;
        public bool showDebugPanel = false;

        private GameController _gc;
        private CanvasGroup mapCanvasGroup;
        private Animator menuAnim, mapAnim;
        private string selectedAttraction;
        private enum AttractionID
        {
            None = 0,
            IceSizzle = 11,
            PieFace = 22,
            HammerDown = 33
        };

        private AttractionID currentAttraction;
        // Use this for initialization
        void Start()
        {
            if (!_gc)
            {
                _gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            }
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRig>();
            }
            if (!mapCanvasGroup)
            {
                //mapCanvasGroup = mapCanvas.GetComponent<CanvasGroup>();
            }

            //if (showDebugPanel)
            //{
            //    debugCanvasGroup.alpha = 1;
            //}
            //else
            //{
            //    debugCanvasGroup.alpha = 0;
            //}
            menuAnim = GetComponent<Animator>();
            //mapAnim = mapCanvas.GetComponent<Animator>();

        }

        //public void WriteToDebug(string s)
        //{
        //    debugText.text = s;
        //}

        //public void ActivateMapMenu(bool b)
        //{
        //    if (b)
        //    {
        //        if (mapCanvas.GetComponent<CanvasGroup>().alpha < 1)
        //        {
        //            mapCanvas.GetComponent<CanvasGroup>().alpha = 1;
        //        }

        //        mapCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //        mapCanvas.GetComponent<CanvasGroup>().interactable = true;
        //    }
        //    else
        //    {
        //        if (mapCanvas.GetComponent<CanvasGroup>().alpha > 0)
        //        {
        //            mapCanvas.GetComponent<CanvasGroup>().alpha = 0;
        //        }
        //        mapCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //        mapCanvas.GetComponent<CanvasGroup>().interactable = false;
        //    }
        //}

        //void AttractionSelection(Collider attractionCol)
        //{
        //    try
        //    {
        //        if (attractionCol)
        //        {
        //            selectedAttraction = attractionCol.GetComponent<Attraction>().sceneName;
        //        }
        //        else
        //        {
        //            currentAttraction = AttractionID.None;
        //            selectedAttraction = "";
        //        }
        //    }
        //    catch (System.Exception e)
        //    {
        //        Debug.Log("Attraction Selection failed with exception: " + e.Message);
        //    }

        //    //Debug.Log("Current Attraction Selected: " + currentAttraction.ToString());
        //}

        /// <summary>
        /// Sets the bool in the MenuSystem's Animtor to the passed in value. If it is true the screen be set to clear. If it is false it will be set to fade to black.
        /// </summary>
        /// <param name="b">True: clear the sceen. False: fade to black.</param>
        public void FadeScreen(bool b)
        {
            menuAnim.SetBool("Fade", b);
        }

        // Update is called once per frame
        void Update()
        {
            if (_gc)
            {
                switch (GameController.State)
                {
                    case GameController.GameState.Active:
                        //if (player.LookingForMap)
                        //{
                        //    //menuAnim.SetBool("LookForMap", true);
                        //}
                        break;
                    case GameController.GameState.Loading:
                        break;
                    case GameController.GameState.Paused:

                        break;
                }
            }

            //if (showDebugPanel)
            //{
            //    if (debugCanvasGroup.alpha < 1)
            //    {
            //        debugCanvasGroup.alpha = 1;
            //    }
            //}
            //else
            //{
            //    if (debugCanvasGroup.alpha > 0)
            //    {
            //        debugCanvasGroup.alpha = 0;
            //    }
            //}
        }

        void LateUpdate()
        {
            if (_gc)
            {
                switch (GameController.State)
                {
                    case GameController.GameState.Active:
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            /* Have some other conditions to hide the map, like test in progress */
                            //if (mapCanvasGroup.alpha > 0)
                            //{
                            //    //ActivateMapMenu(false);
                            //}
                        }
                        else
                        {
                            //if (mapCanvasGroup.alpha < 1)
                            //{
                            //    //ActivateMapMenu(true);
                            //}
                        }
                        //AttractionSelection(player.AttractionCollider);
                        break;
                    case GameController.GameState.Loading:
                        break;
                    case GameController.GameState.Paused:
                        if (_gc.SceneReady)
                        {
                            FadeScreen(true);
                        }
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            if (_gc.GamePaused)
                            {

                            }
                        }
                        break;
                }
            }
        }

        public bool ScreenClear
        {
            get { return fullCanvas.ScreenClear; }
        }

        public string SelectedAttraction
        {
            get { return selectedAttraction; }
        }
    }

}
