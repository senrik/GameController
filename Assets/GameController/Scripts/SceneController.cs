using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController {
    /// <summary>
    /// Placed as a part of every scene that is used to get a scene ready for the player. Should handle initial load-in and state transition from loading to paused.
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        public GameObject gameControllerPrefab;
        public Transform playerSpawn;

        protected GameController _gc;
        protected PlayerRig _player;
        private bool sceneReady, playerPlaced;
        private static SceneController currentInstance;

        void Awake()
        {
            currentInstance = this;
        }

        // Use this for initialization
        protected void Start()
        {
            if (GameObject.FindGameObjectWithTag("GameController"))
            {
                _gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            }
            else
            {
                _gc = Instantiate(gameControllerPrefab).GetComponent<GameController>();
            }
            
            if(GameObject.FindGameObjectWithTag("Player"))
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRig>();
            }

        }

        protected void PlacePlayer()
        {
            if(_player && !playerPlaced)
            {
                Debug.Log("Setting player to spawn point: " + playerSpawn.position);
                _player.SetPlayerPosition(playerSpawn);
                playerPlaced = true;
            }
        }

        // Update is called once per frame
        protected void Update()
        {

            if(!_player)
            {
                if (GameObject.FindGameObjectWithTag("Player"))
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRig>();
                }
            }
            else
            {
                switch (GameController.State)
                {
                    case GameState.Active:
                        break;
                    case GameState.Loading:
                        if (sceneReady && !_gc.SceneReady && !_gc.LoadingScene)
                        {
                            _gc.SceneReady = true;
                        }

                        if (!sceneReady && playerPlaced)
                        {
                            sceneReady = true;
                            Debug.Log("Scene Ready!");
                        }
                        else
                        {
                            if(!playerPlaced)
                            {
                                PlacePlayer();
                            }
                        }
                        break;
                    case GameState.Paused:
                        break;

                }
            }

        }

        public static SceneController CurrentSceneController
        {
            get { return currentInstance; }
        }
    }
}