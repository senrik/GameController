﻿using System.Collections;
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
        protected bool sceneReady, playerPlaced;
        private static SceneController instance;

        protected void Awake()
        {
            instance = this;
            if(!playerSpawn)
            {
                playerSpawn = transform;
            }

            if (GameController.ActiveGameController)
            {
                _gc = GameController.ActiveGameController;
            }
        }

        // Use this for initialization
        protected void Start()
        {
            /* There was no game controller present at the Awake time */
            if (_gc == null)
            {
                if (GameController.ActiveGameController)
                {
                    _gc = GameController.ActiveGameController;
                }
                else
                {
                    _gc = Instantiate(gameControllerPrefab).GetComponent<GameController>();
                }
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
                //Debug.Log("Setting player to spawn point: " + playerSpawn.position);
                _player.SetPlayerPosition(playerSpawn);
                playerPlaced = true;
            }
        }

        public static SceneController ActiveSceneController
        {
            get { return instance; }
        }

        /// <summary>
        /// Update function that is called once per frame when the GameController is in the Active State.
        /// </summary>
        protected void ActiveUpdate()
        {

        }

        /// <summary>
        /// Update function that is called once per frame when the GameController is in the Loading State. Base sets the GameController's SceneReady function to true if the SceneController has set its sceneReady bool to true.
        /// </summary>
        protected void LoadingUpdate()
        {
            if (sceneReady && !_gc.SceneReady && !_gc.LoadingScene)
            {
                _gc.SceneReady = true;
            }
        }

        /// <summary>
        /// Update function that is called once per frame when the GameController is in the Paused State.
        /// </summary>
        protected void PausedUpdate()
        {

        }

        // Update is called once per frame
        protected void Update()
        {

            if(!_player)
            {
                if (GameController.ActiveGameController.ActivePlayer != null)
                {
                    _player = GameController.ActiveGameController.ActivePlayer;
                }
            }

        }

        public static SceneController CurrentSceneController
        {
            get { return instance; }
        }
    }
}