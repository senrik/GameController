using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class PlayerRig : MonoBehaviour
    {
        public bool DEBUG_MODE = false;
        public MenuSystem menuSystem;

        private GameController _gc;

        // Use this for initialization
        void Start()
        {

        }

        public void SetGC(GameController gc)
        {
            _gc = gc;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                _gc.LoadMe("PlayScene");
            }
        }
    }
}

