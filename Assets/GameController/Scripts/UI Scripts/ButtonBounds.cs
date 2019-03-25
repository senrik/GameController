using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ButtonBounds : MonoBehaviour {

    public RectTransform parentTrans;
    public float canvasScale;
    private BoxCollider _bounds;
    private UIInteractiveElement _elem;

    void Awake()
    {
        //Debug.Log("Setting interact bounds for the " + transform.parent.name);
        _bounds = (GetComponent<BoxCollider>()) ? GetComponent<BoxCollider>() : null;
        //Debug.Log("Current Bounds: " + _bounds.size.ToString());
        try
        {
            // Set Bounds based off of rect transform and canvas scale
            Vector2 tempSize = parentTrans.sizeDelta;
           // Debug.Log("Parent's size: " + tempSize.ToString() + " of " + transform.parent.name);
            if(tempSize.x > 0 && tempSize.y > 0)
            {
                _bounds.size = tempSize;
                
            }
            
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        _elem = (transform.parent.GetComponent<UIInteractiveElement>()) ? transform.parent.GetComponent<UIInteractiveElement>() : null;
    }

    public UIInteractiveElement InteractableElement
    {
        get { return _elem; }
    }
}
