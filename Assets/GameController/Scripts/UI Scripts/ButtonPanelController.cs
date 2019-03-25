using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelController : MonoBehaviour {

    public List<UIButtonElement> buttons;

    void Awake()
    {
        foreach(UIButtonElement elem in GetComponentsInChildren<UIButtonElement>())
        {
            if(!buttons.Contains(elem))
            {
                buttons.Add(elem);
            }
        }
    }

    public UIButtonElement GetButtonByTag(string tag)
    {
        foreach(UIButtonElement b in buttons)
        {
            if(b.tag == tag)
            {
                return b;
            }
        }

        return null;
    }
    
}
