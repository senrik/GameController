using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInterativeElement : MonoBehaviour {

    public UnityEvent OnInteract;
    private Animator _anim;

	// Use this for initialization
	void Start () {
		if(GetComponent<Animator>())
        {
            _anim = GetComponent<Animator>();
        }
	}
	public void OnHoverStart()
    {
        Debug.Log("OnHoverStart!");
        if(!_anim.GetBool("selected"))
        {
            _anim.SetBool("selected", true);
        }
    }

    public void OnHoverEnd()
    {
        if (_anim.GetBool("selected"))
        {
            _anim.SetBool("selected", false);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
