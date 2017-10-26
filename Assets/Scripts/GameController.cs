using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameController
{
    public class GameController : MonoBehaviour
    {

        public bool DEBUG_MODE = false;
        public enum GameState
        {
            Active,
            Paused,
            Loading,
        };

        public GameObject playerPrefab;
        public List<Transform> playerSpawns;
        public List<SceneCard> scenes;
        private static GameState state = GameState.Loading;
        private bool loadScene, pauseGame /*, mapActive*/;
        private static bool sceneReady = true;
        private string sceneToLoad;
        private MenuSystem _ms;
        private GameObject player;


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
        public void SetPlayerSpawns(List<Transform> spawns)
        {
            foreach (Transform t in spawns)
            {
                Transform temp = t;
                playerSpawns.Add(temp);
            }

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

        void OnEnable()
        {
            //SceneManager.sceneLoaded += GetComponent<SessionController>().LevelWasLoaded;
        }

        void OnDisable()
        {
            //SceneManager.sceneLoaded -= GetComponent<SessionController>().LevelWasLoaded;
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
            if (playerSpawns.Count < 1)
            {
                playerSpawns.Add(transform);
            }
            if (!player)
            {
                player = Instantiate(playerPrefab);
                _ms = player.GetComponent<PlayerRig>().menuSystem;
                if (DEBUG_MODE)
                {
                    if (!player.GetComponent<PlayerRig>().DEBUG_MODE)
                    {
                        player.GetComponent<PlayerRig>().DEBUG_MODE = true;
                    }
                }
            }
            if (!_ms && GameObject.FindGameObjectWithTag("MenuSystem"))
            {
                //_ms = GameObject.FindGameObjectWithTag("MenuSystem").GetComponent<MenuSystem>();
            }
            loadScene = false;
            pauseGame = false;
            //mapActive = true;
        }

        public void LoadMe(string scene)
        {
            Debug.Log(string.Format("Attempting to load: {0}", scene));
            _ms.FadeScreen(false);
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
                //player.GetComponent<PlayerRig>().PrintToDebug(string.Format("MenuSystem Not Set"));
            }
            else
            {
                switch (state)
                {

                    case GameState.Active:
                        #region Active
                        //player.GetComponent<PlayerRig>().PrintToDebug(string.Format("GameState: {0}", state.ToString()));
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
#if (UNITY_EDITOR)
                        if (Input.GetButtonDown("Cancel"))
                        {
                            state = GameState.Paused;
                            pauseGame = true;
                        }
#else
                    if (Input.GetButtonDown("Cancel"))
                    {
                        LoadMe("MMscene01");
                    }
#endif
                        //if (mapActive)
                        //{
                        //    if (Input.GetButtonDown("Confirm") || Input.GetButtonDown("Fire1"))
                        //    {
                        //        if (_ms.SelectedAttraction.Length > 0)
                        //        {
                        //            LoadMe(_ms.SelectedAttraction);
                        //        }
                        //    }
                        //}

                        //if (SceneManager.GetActiveScene().buildIndex > 0)
                        //{
                        //    mapActive = false;
                        //    _ms.ActivateMapMenu(false);
                        //}
                        #endregion
                        break;
                    case GameState.Loading:
                        #region Loading
                        //player.GetComponent<PlayerRig>().PrintToDebug(string.Format("GameState: {0}", state.ToString()));
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
                        //player.GetComponent<PlayerRig>().PrintToDebug(string.Format("GameState: {0}", state.ToString()));
                        // If the current scene is not the main menu scene
                        if (SceneManager.GetActiveScene().buildIndex > 0)
                        {
                            // If the screen is clear 
                            if (_ms.ScreenClear)
                            {
                                //player.GetComponent<PlayerRig>().PrintToDebug("Screen is clear.");
                                // The game is flagged to be paused
                                if (pauseGame)
                                {
                                    //player.GetComponent<PlayerRig>().PrintToDebug("Game is flagged to be paused.");
                                    // Pause the game
                                    PauseGame(true);
                                    // If the player presses the pause button
                                    if (Input.GetButtonDown("Cancel"))
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
                        // If it is the main menu scene
                        else
                        {
                            // If the screen is clear
                            if (_ms.ScreenClear)
                            {
                                //player.GetComponent<PlayerRig>().PrintToDebug("Screen is clear.");
                                // The game is flagged to be paused
                                if (pauseGame)
                                {
                                    //player.GetComponent<PlayerRig>().PrintToDebug("Game is flagged to be paused.");
                                    // If the player presses the pause button
                                    if (Input.GetButtonDown("Cancel"))
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
                                    //player.GetComponent<PlayerRig>().PrintToDebug("Game is not set to be paused.");
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
            //Debug.Log(string.Format("SceneReady: {0}", sceneReady));
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
    }
}

