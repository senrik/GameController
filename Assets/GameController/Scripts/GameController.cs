using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameController
{
    public class GameController : MonoBehaviour
    {

        public bool DEBUG_MODE = false;
        

        public GameObject playerPrefab;
        public List<SceneCard> scenes;
        private static GameState state = GameState.Loading;
        private bool loadScene, pauseGame;
        private static bool sceneReady = false;
        private string sceneToLoad;
        
        private MenuSystem _ms;
        private GameObject player;
        private SceneController currentSceneController;


        #region Coroutines
        IEnumerator LoadScene(string s)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(s);
            ao.allowSceneActivation = false;
            sceneReady = false;
            state = GameState.Loading;

            while (!ao.isDone)
            {
                float progress = Mathf.Clamp01(ao.progress / 0.9f);
                //Debug.Log("Loading Progress: " + (progress * 100) + "%");

                if (ao.progress == 0.9f)
                {
                    ao.allowSceneActivation = true;
                }

                yield return null;
            }
            Start();
        }
        #endregion

        void StartLoadScene(string scene)
        {
            if (!_ms.ScreenClear)
            {
                StartCoroutine(LoadScene(scene));
            }
        }
        public void SetPlaySceneName(string n)
        {
            _ms.PlaySceneName = n;
            _ms.BindMainMenu = true;
            _ms.BindPauseMenu = true;
        }
        public void PauseGame(bool p)
        {
            if (p)
            {
                //Debug.Log("PauseGame called");
                if (Time.timeScale > 0)
                {
                    Time.timeScale = 0;
                }
                /* If there are any objects to disable or put to sleep, put those calls here. */

                if (Cursor.lockState != CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                //player.SetActive(false);
            }
            else
            {
                //Debug.Log("ResumeGame called.");
                if (Time.timeScale < 1)
                {
                    Time.timeScale = 1;
                }
                if (Cursor.lockState != CursorLockMode.Locked)
                {
                    //Debug.Log("Locking the cursor.");
                    Cursor.lockState = CursorLockMode.Locked;
                }

                pauseGame = false;
                //player.SetActive(true);
                state = GameState.Active;
            }

        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);

            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (GameObject.FindGameObjectWithTag("SceneController"))
            {
                currentSceneController = SceneController.CurrentSceneController; //GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
            }

            if (!player)
            {
                player = Instantiate(playerPrefab);
                _ms = player.GetComponent<PlayerRig>().menuSystem;
                player.GetComponent<PlayerRig>().SetGC(this);
                if (DEBUG_MODE)
                {
                    if (!player.GetComponent<PlayerRig>().DEBUG_MODE)
                    {
                        player.GetComponent<PlayerRig>().DEBUG_MODE = true;
                    }
                }

                player.transform.position = currentSceneController.playerSpawn.position;
            }
            loadScene = false;
            pauseGame = false;
        }

        public void LoadMe(string scene)
        {
            Debug.Log(string.Format("Attempting to load: {0}", scene));
            _ms.FadeScreen(false);
            _ms.FadeInStarted = false;
            loadScene = true;
            sceneToLoad = scene;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_ms)
            {
                if (GameObject.FindGameObjectWithTag("MenuSystem"))
                {
                    _ms = GameObject.FindGameObjectWithTag("MenuSystem").GetComponent<MenuSystem>();
                }
            }
            else
            {
                switch (state)
                {

                    case GameState.Active:
                        #region Active
                        // if the game is flagged to load a scene
                        if (loadScene)
                        {
                            StartLoadScene(sceneToLoad);
                        }
                        else
                        {
                            if (player)
                            {
                                if (!player.activeSelf)
                                {
                                    player.SetActive(true);
                                }
                                
                            }
                        }
                        // If we are not on the main menu
                        if(SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            // If the Pause button was pressed
                            if (player.GetComponent<PlayerRig>().PausePressed)
                            {
                                // Reset the pause button flags
                                player.GetComponent<PlayerRig>().ResetPausePressed();
                                // Switch to the Paused state
                                state = GameState.Paused;
                                // Flag the GameController to pause the game (timeScale = 0)
                                pauseGame = true;
                            }
                        }
                        #endregion
                        break;
                    case GameState.Loading:
                        #region Loading
                        // If the current scene is not the main menu scene
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            if (sceneReady)
                            {
                                state = GameState.Paused;
                            }
                        }
                        else
                        {
                            if (sceneReady)
                            {
                                state = GameState.Paused;
                            }


                        }
                        #endregion
                        break;
                    case GameState.Paused:
                        #region Paused
                        // If the current scene is not the main menu scene
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            // If the screen is clear 
                            if (_ms.ScreenClear)
                            {
                                // The game is flagged to be paused
                                if (pauseGame)
                                {
                                    // Pause the game
                                    PauseGame(true);
                                    // If the player presses the pause button
                                    if (player.GetComponent<PlayerRig>().PausePressed)
                                    {
                                        player.GetComponent<PlayerRig>().ResetPausePressed();
                                        // Flag to resume the game
                                        PauseGame(false);
                                    }
                                    // If the game is supposed to load a scene
                                    if (loadScene)
                                    {
                                        // Unpause the game
                                        PauseGame(false);
                                        // Load the specified scene
                                        StartLoadScene(sceneToLoad);
                                    }
                                }
                                // If the game is not flagged to be paused (this is to catch the initial load-in from another scene)
                                else
                                {
                                    // Set the state to active
                                    state = GameState.Active;
                                }

                            }

                        }
                        // If it is the main menu scene
                        else
                        {
                            // If the screen is clear
                            if (_ms.ScreenClear)
                            {
                                // The game is flagged to be paused
                                if (pauseGame)
                                {
                                    // If the player presses the pause button
                                    if (Input.GetButtonDown("Pause"))
                                    {
                                        // Flag to resume the game
                                        PauseGame(false);
                                    }
                                    // If the game is supposed to load a scene
                                    if (loadScene)
                                    {
                                        // Unpause the game
                                        PauseGame(false);
                                        // Load the specified scene
                                        StartLoadScene(sceneToLoad);
                                    }
                                }
                                // If the game is not flagged to be paused (this is to catch the initial load-in from another scene)
                                else
                                {
                                    // Set the state to active
                                    state = GameState.Active;
                                }

                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            }

            Debug.Log("Game State: " + state.ToString());
        }

        public static GameState State
        {
            get { return state; }
        }

        public bool SceneReady
        {
            get { return sceneReady; }
            set { sceneReady = value; }
        }

        public bool GamePaused
        {
            get { return pauseGame; }
        }

        public bool LoadingScene
        {
            get { return loadScene; }
        }
    }
}

