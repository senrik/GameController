using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputHandler : MonoBehaviour {

    public ControllerInputHandler otherController;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private bool applicationMenuPressed = false, triggerPressed = false, acceptingInput;
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

	// Update is called once per frame
	void Update () {
        if (Controller.GetAxis() != Vector2.zero)
        {
            //Debug.Log(gameObject.name + Controller.GetAxis());
        }

        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
            triggerPressed = true;
        }

        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
            triggerPressed = false;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            applicationMenuPressed = true;
            Debug.Log("ERMAHGERD");
        }
		
	}

    public bool PauseButtonPressed
    {
        get { return applicationMenuPressed; }
        set { applicationMenuPressed = value; }
    }
    public bool AcceptInput
    {
        get { return acceptingInput; }
        set { acceptingInput = value; }
    }
    public bool TriggerPressed
    {
        get { return triggerPressed; }
    }
    public SteamVR_Controller.Device Stick
    {
        get { return Controller; }
    }
}
