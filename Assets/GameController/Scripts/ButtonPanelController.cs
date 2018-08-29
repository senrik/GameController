using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelController : MonoBehaviour {

    public List<UIInterativeElement> buttons;

    void Awake()
    {
        foreach(UIInterativeElement elem in GetComponentsInChildren<UIInterativeElement>())
        {
            if(!buttons.Contains(elem))
            {
                buttons.Add(elem);
            }
        }
    }

    public UIInterativeElement GetButtonByTag(string tag)
    {
        foreach(UIInterativeElement b in buttons)
        {
            if(b.tag == tag)
            {
                return b;
            }
        }

        return null;
    }
    
}
