using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameController
{
    public class MainMenuController : MenuController
    {

        new void Awake()
        {
            base.Awake();

        }

        /// <summary>
        /// Toggles the display of the menu.
        /// </summary>
        /// <param name="t">If t is true, then it will display the menu. If t is false, then it will hide the menu.</param>
        new public void ToggleMenu(bool t)
        {
            base.ToggleMenu(t);
        }

        public UIInteractiveElement PlayButton
        {
            get {
                for(int i = 0; i < buttonPanel.buttons.Count; i++)
                {
                    if (buttonPanel.buttons[i].CompareTag("PlayButton"))
                    {
                        //Debug.Log("PlayButton found!");
                        return buttonPanel.buttons[i];
                    }
                }
                //Debug.Log("PlayButton not found!");
                return null;
            }
        }

        //public bool HUDAssist
        //{
            
        //}
    }
}

