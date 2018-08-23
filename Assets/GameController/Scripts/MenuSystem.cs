using UnityEngine;
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
        public bool showDebugPanel = false;
        private bool mainMenuBound = false, bindMainMenu = false, pauseMenuBound = false, bindPauseMenu;
        private GameController _gc;
        private CanvasGroup mainMenuCanvasGroup;
        private Animator menuAnim, mapAnim;
        private string playSceneName;
        
        
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
            if (!mainMenuCanvasGroup)
            {
                mainMenuCanvasGroup = mainMenu.GetComponent<CanvasGroup>();
            }

            menuAnim = GetComponent<Animator>();
            
        }

        private void BindMainMenuActions()
        {
            Debug.Log("Binding Main Menu Buttons");
            try
            {
                mainMenu.PlayButton.OnInteract.AddListener(delegate { _gc.LoadMe(playSceneName); });
            }
            catch(System.Exception e)
            {
                Debug.Log(string.Format("Exception \"{0}\" encountered while trying to set the play button action.", e.Message));
            }

            mainMenuBound = true;
            
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

        // Update is called once per frame
        void Update()
        {
            if (_gc)
            {
                switch (GameController.State)
                {
                    case GameState.Active:
                        if (bindMainMenu && !mainMenuBound)
                        {
                           BindMainMenuActions();
                        }

                        if(bindPauseMenu && !pauseMenuBound)
                        {
                            BindPauseMenuActions();
                        }
                        break;
                    case GameState.Loading:
                        if (bindMainMenu && !mainMenuBound)
                        {
                            BindMainMenuActions();
                        }

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
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            mainMenu.ToggleMenu(false);
                            
                        }
                        else
                        {

                            mainMenu.ToggleMenu(true);
                        }

                        pauseMenu.ToggleMenu(false);
                        break;
                    case GameState.Loading:
                        break;
                    case GameState.Paused:
                        if (_gc.SceneReady)
                        {
                            FadeScreen(true);
                        }
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            if (_gc.GamePaused)
                            {
                                pauseMenu.ToggleMenu(true);
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
