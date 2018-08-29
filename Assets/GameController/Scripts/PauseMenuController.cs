using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MenuController {

    new void Awake()
    {
        base.Awake();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public UIInterativeElement MainMenuButton
    {
        get
        {
            for (int i = 0; i < buttonPanel.buttons.Count; i++)
            {
                if (GetButtonByTag("MainMenuButton"))
                {
                    Debug.Log("MainMenuButton found!");
                    return GetButtonByTag("MainMenuButton");
                }
            }
            Debug.Log("MainMenuButton not found!");
            return null;
        }
    }

    public UIInterativeElement ResumeButton
    {
        get
        {
            for (int i = 0; i < buttonPanel.buttons.Count; i++)
            {
                if (GetButtonByTag("ResumeButton"))
                {
                    Debug.Log("ResumeButton found!");
                    return GetButtonByTag("ResumeButton");
                }
            }
            Debug.Log("ResumeButton not found!");
            return null;
        }
    }
}
