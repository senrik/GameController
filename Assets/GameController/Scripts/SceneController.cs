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


        protected GameController _gc;
        private bool sceneReady;
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
            

        }

        // Update is called once per frame
        protected void Update()
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
    }
}