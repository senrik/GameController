using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public ButtonPanelController buttonPanel;
    protected CanvasGroup cnvsGrp;

    protected void Awake()
    {
        cnvsGrp = GetComponent<CanvasGroup>();
    }

    public void ToggleMenu(bool t)
    {
        if (t)
        {
            if (cnvsGrp.alpha < 1)
            {
                if (!buttonPanel.gameObject.activeSelf)
                {
                    buttonPanel.gameObject.SetActive(true);
                }
                cnvsGrp.alpha = 1;
                cnvsGrp.interactable = true;
                cnvsGrp.blocksRaycasts = true;
            }
        }
        else
        {
            if (cnvsGrp.alpha > 0)
            {
                if (buttonPanel.gameObject.activeSelf)
                {
                    buttonPanel.gameObject.SetActive(false);
                }
                cnvsGrp.alpha = 0;
                cnvsGrp.interactable = false;
                cnvsGrp.blocksRaycasts = false;
            }
        }
    }

    public UIInterativeElement GetButtonByTag(string tag)
    {
        if(buttonPanel.GetButtonByTag(tag) != null)
        {
            return buttonPanel.GetButtonByTag(tag);
        }
        else
        {
            throw new System.NullReferenceException("No button with tag: " + tag + " was found in the Button Panel.");
        }
    }
}
