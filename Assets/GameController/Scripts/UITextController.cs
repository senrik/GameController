using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : UIElementController {

    private Text _uiText;

	protected void Awake()
    {
        base.Awake();
        _uiText = (GetComponent<Text>()) ? GetComponent<Text>() : gameObject.AddComponent<Text>();
    }

    /// <summary>
    /// Assigns string s as the visible text to the text element.
    /// </summary>
    /// <param name="s">String to be assigned to the text element.</param>
    protected void AssignMessage(string s)
    {
        if(!Visible)
        {
            _uiText.text = s;
        }
    }
}
