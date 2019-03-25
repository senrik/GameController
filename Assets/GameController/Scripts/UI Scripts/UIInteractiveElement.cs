using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInteractiveElement : UIElementController {

    public UnityEvent OnInteract;

	// Use this for initialization
	protected void Start () {
		
	}

	public void OnHoverStart()
    {
        
    }

    public void OnHoverEnd()
    {
        
    }

    public bool Interactable
    {
        get;
        set;
    }
}
