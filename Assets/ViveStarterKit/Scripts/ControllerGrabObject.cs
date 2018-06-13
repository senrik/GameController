using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    public ViveControllerInputTest inputHandler;

    private GameObject collidingObject;
    private GameObject objectInHand;
    private FixedJoint _fixedJoint;


	private SteamVR_Controller.Device Controller
    {
        get { return inputHandler.Stick; }
    }

    private void Awake()
    {
        if(!GetComponent<FixedJoint>())
        {
            _fixedJoint = gameObject.AddComponent<FixedJoint>();

            _fixedJoint.breakForce = 20000;
            _fixedJoint.breakTorque = 20000;
        }
    }

    private void SetCollidingObject(Collider col)
    {
        if(collidingObject || !col.GetComponent<Rigidbody>() || !col.transform.parent)
        {
            return;
        }
        collidingObject = col.transform.parent.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if(!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;

        _fixedJoint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }


    private void ReleaseObject()
    {
        if(GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }

    // Update is called once per frame
    void Update () {
		if(Controller.GetHairTriggerDown())
        {
            if(collidingObject)
            {
                GrabObject();
            }
        }

        if(Controller.GetHairTriggerUp())
        {
            if(objectInHand)
            {
                ReleaseObject();
            }
        }
	}
}
