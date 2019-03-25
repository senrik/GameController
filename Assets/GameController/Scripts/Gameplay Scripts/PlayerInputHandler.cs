using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class PlayerInputHandler : MonoBehaviour
    {

        public ControllerInputHandler leftController, rightController;

        private bool pausePressed = false;

        // Use this for initialization
        void Start()
        {

        }

        public void ResetPausePressed()
        {
            leftController.PauseButtonPressed = false;
            rightController.PauseButtonPressed = false;
            pausePressed = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (leftController.PauseButtonPressed || rightController.PauseButtonPressed)
            {
                pausePressed = true;
                //Debug.Log("Pause Button Pressed!");
            }
            else
            {
                pausePressed = false;
            }

        }

        public void ToggleTeleport(bool t)
        {
            leftController.ToggleTeleport(t);
            rightController.ToggleTeleport(t);
        }

        public void ToggleControllers(bool t)
        {
            leftController.ToggleController(t);
            rightController.ToggleController(t);
        }

        public bool MenuButtonPressed
        {
            get { return pausePressed; }
        }
    }
}
