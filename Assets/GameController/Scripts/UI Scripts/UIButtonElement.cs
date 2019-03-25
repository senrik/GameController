using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonElement : UIInteractiveElement {

    private Animator _anim;
    private bool active_anim;
    // Use this for initialization
    new void Start () {
        base.Start();
        if (GetComponent<Animator>())
        {
            _anim = GetComponent<Animator>();
        }
    }

    new public void OnHoverStart()
    {
        //Debug.Log("OnHoverStart!");
        if (!_anim.GetBool("selected"))
        {
            _anim.SetBool("selected", true);
        }
    }

    new public void OnHoverEnd()
    {
        if (_anim.GetBool("selected"))
        {
            _anim.SetBool("selected", false);
        }
    }

    new public bool Interactable
    {
        get
        {
            if (!_anim)
            {
                _anim = GetComponent<Animator>();
            }
            active_anim = _anim.GetBool("active");
            return active_anim;
        }
        set
        {
            if (!_anim)
            {
                _anim = GetComponent<Animator>();
            }
            active_anim = value;
            _anim.SetBool("active", active_anim);
        }
    }
}
