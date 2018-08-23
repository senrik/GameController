﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class PlayerRig : MonoBehaviour
    {
        public bool DEBUG_MODE = false;
        public MenuSystem menuSystem;

        private PlayerInputHandler _input;
        private GameController _gc;

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
            _input = GetComponent<PlayerInputHandler>();
        }

        public void SetGC(GameController gc)
        {
            _gc = gc;
        }
        public void ResetPausePressed()
        {
            _input.ResetPausePressed();
        }
        // Update is called once per frame
        void Update()
        {
            switch (GameController.State)
            {
                case GameState.Active:

                    break;
            }
        }

        public bool PausePressed
        {
            get { return _input.MenuButtonPressed; }
        }
    }
}

