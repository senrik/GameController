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


        private GameController _gc;
        private bool sceneReady;
        // Use this for initialization
        protected void Start()
        {
            if (GameController.ActiveGameController != null)
            {
                _gc = GameController.ActiveGameController;
            }
            else
            {
                _gc = Instantiate(gameControllerPrefab).GetComponent<GameController>();
            }
            

        }

        protected virtual void ActiveUpdate()
        {
            
        }

        protected virtual void LoadingUpdate()
        {
            if (sceneReady && !_gc.SceneReady)
            {
                _gc.SceneReady = true;
            }

            if (!sceneReady)
            {
                sceneReady = true;
            }
        }

        protected virtual void PausedUpdate()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            

            switch(GameController.State)
            {
                case GameState.Active:
                    #region ActiveUpdate
                    ActiveUpdate();
                    #endregion
                    break;
                case GameState.Loading:
                    #region LoadingUpdate
                    LoadingUpdate();
                    #endregion
                    break;
                case GameState.Paused:
                    #region PausedUpdate
                    PausedUpdate();
                    #endregion
                    break;
            }
        }
    }
}