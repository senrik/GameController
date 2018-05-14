using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameController
{
    public class MainMenuController : MonoBehaviour
    {
        public ButtonPanelController buttonPanel;
        private CanvasGroup cnvsGrp;

        void Awake()
        {
            cnvsGrp = GetComponent<CanvasGroup>();
        }
        // Use this for initialization
        void Start()
        {

        }
        /// <summary>
        /// Toggles the display of the main menu.
        /// </summary>
        /// <param name="t">If t is true, then it will display the menu. If t is false, then it will hide the menu.</param>
        public void ToggleMainMenu(bool t)
        {
            if (t)
            {
                if (cnvsGrp.alpha < 1)
                {
                    cnvsGrp.alpha = 1;
                    cnvsGrp.interactable = true;
                    cnvsGrp.blocksRaycasts = true;
                }
            }
            else
            {
                if (cnvsGrp.alpha > 0)
                {
                    cnvsGrp.alpha = 0;
                    cnvsGrp.interactable = false;
                    cnvsGrp.blocksRaycasts = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Button PlayButton
        {
            get {
                for(int i = 0; i < buttonPanel.buttons.Count; i++)
                {
                    if (buttonPanel.buttons[i].CompareTag("PlayButton"))
                    {
                        Debug.Log("PlayButton found!");
                        return buttonPanel.buttons[i];
                    }
                }
                Debug.Log("PlayButton not found!");
                return null;
            }
        }
    }
}

