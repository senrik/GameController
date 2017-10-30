using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController {
    /// <summary>
    /// Placed as a part of every scene that is used to get a scene ready for the player. Should handle initial load-in and state transition from loading to paused.
    /// </summary>
    public class SceneController : MonoBehaviour
    {

        private GameController _gc;
        private bool sceneReady;
        // Use this for initialization
        void Start()
        {
            _gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        }

        // Update is called once per frame
        void Update()
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