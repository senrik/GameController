using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementController : MonoBehaviour {


    private CanvasGroup _canvasGroup;

    protected void Awake()
    {
        _canvasGroup = (GetComponent<CanvasGroup>()) ? GetComponent<CanvasGroup>() : gameObject.AddComponent<CanvasGroup>();
        
    }
    /// <summary>
    /// Shows or hides the UI element.
    /// </summary>
    /// <param name="t">On true the element is shown if it is not already doing so. On false the element is hidden unless it is already hidden.</param>
    protected void ToggleElement(bool t)
    {
        if(t)
        {
            if(_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha = 1;
            }
        }
        else
        {
            if(_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha = 0;
            }
        }
    }
    /// <summary>
    /// Returns true when the UI element is visible, false when the element is hidden.
    /// </summary>
    public bool Visible
    {
        get { return (_canvasGroup.alpha > 0) ? true : false; }
    }
}
