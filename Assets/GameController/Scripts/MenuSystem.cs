using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GameController
{
    public class MenuSystem : MonoBehaviour
    {

        public FullscreenCanvas fullCanvas;
        public MainMenuController mainMenu;
        public PauseMenuController pauseMenu;
        //public GameObject mapCanvas;
        //public CanvasGroup debugCanvasGroup;
        //public Text debugText;
        public PlayerRig player;
        public float blackoutTime = 2.0f;
        public bool showDebugPanel = false;
        private bool mainMenuBound = false, bindMainMenu = false, pauseMenuBound = false, bindPauseMenu, pauseMenuPlaced;
        private bool fadeInStarted = false;
        private GameController _gc;
        //private CanvasGroup mainMenuCanvasGroup;
        private Animator menuAnim, mapAnim;
        private string playSceneName;
        private Vector3 tempRot, tempPos;
        
        // Use this for initialization
        void Start()
        {
            try
            {
                if (!_gc)
                {
                    if (GameObject.FindGameObjectWithTag("GameController"))
                    {
                        _gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                    }
                    else
                    {
                        throw new System.NullReferenceException("No initialized GameController Object found!");
                    }

                }
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }
            
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRig>();
             
            }

            //if (!mainMenuCanvasGroup)
            //{
            //    mainMenuCanvasGroup = mainMenu.GetComponent<CanvasGroup>();
            //}

            menuAnim = GetComponent<Animator>();
            tempRot = new Vector3();
            tempPos = new Vector3();

            if(!pauseMenuBound)
            {
                BindPauseMenuActions();
            }
        }

        IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(blackoutTime);
            FadeScreen(true);
        }

        

        private void BindPauseMenuActions()
        {
            Debug.Log("Binding Pause Menu Buttons");
            try
            {
                pauseMenu.MainMenuButton.OnInteract.AddListener(delegate { _gc.LoadMe("MainMenuScene"); });
                pauseMenu.ResumeButton.OnInteract.AddListener(delegate { _gc.PauseGame(false); });
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

            pauseMenuBound = true;
        }

        /// <summary>
        /// Sets the bool in the MenuSystem's Animtor to the passed in value. If it is true the screen be set to clear. If it is false it will be set to fade to black.
        /// </summary>
        /// <param name="b">True: clear the sceen. False: fade to black.</param>
        public void FadeScreen(bool b)
        {
            fullCanvas.ScreenFade(b);
        }

        private void PlacePauseMenu()
        {
            if(!pauseMenuPlaced)
            {
                pauseMenu.transform.position = player.headTransform.position;
                pauseMenu.transform.rotation = player.headTransform.rotation;
                tempRot = pauseMenu.transform.eulerAngles;
                tempRot.z = 0;
                tempRot.x = 0;
                pauseMenu.transform.eulerAngles = tempRot;
                tempPos = pauseMenu.transform.position + pauseMenu.transform.forward * 1.5f;
                tempPos.y = 2.0f;
                pauseMenu.transform.position = tempPos;
                pauseMenuPlaced = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_gc)
            {
                switch (GameController.State)
                {
                    case GameState.Active:
                        if(bindPauseMenu && !pauseMenuBound)
                        {
                            BindPauseMenuActions();
                        }
                        break;
                    case GameState.Loading:

                        if (bindPauseMenu && !pauseMenuBound)
                        {
                            BindPauseMenuActions();
                        }
                        break;
                    case GameState.Paused:
                        
                        break;
                }
            }

        }

        void LateUpdate()
        {
            if (_gc)
            {
                switch (GameController.State)
                {
                    case GameState.Active:
                        pauseMenu.ToggleMenu(false);
                        if(pauseMenuPlaced)
                        {
                            pauseMenuPlaced = false;
                        }
                        break;
                    case GameState.Loading:
                        break;
                    case GameState.Paused:
                        if (_gc.SceneReady)
                        {
                            if(!fadeInStarted)
                            {
                                StartCoroutine(FadeIn());
                                fadeInStarted = true;
                            }
                            
                        }
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            if (_gc.GamePaused)
                            {
                                if(!pauseMenuPlaced)
                                {
                                    PlacePauseMenu();
                                }
                                else
                                {
                                    pauseMenu.ToggleMenu(true);
                                }
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

        public bool BindMainMenu
        {
            get { return bindMainMenu; }
            set { bindMainMenu = value; }
        }
        public bool FadeInStarted
        {
            get { return fadeInStarted; }
            set { fadeInStarted = value; }
        }
        public bool BindPauseMenu
        {
            get { return bindPauseMenu; }
            set { bindPauseMenu = value; }
        }

        public string PlaySceneName
        {
            get { return playSceneName; }
            set { playSceneName = value; }
        }
    }

}
