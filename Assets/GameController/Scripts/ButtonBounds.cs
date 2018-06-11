using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBounds : MonoBehaviour {

    public float canvasScale;
    private BoxCollider bounds;


    void Awake()
    {
        bounds = (GetComponent<BoxCollider>()) ? GetComponent<BoxCollider>() : null;
        try
        {
            // Set Bounds based off of rect transform and canvas scale
            Vector3 temp_size = transform.parent.GetComponent<RectTransform>().sizeDelta;
            Debug.Log(transform.parent.GetComponent<RectTransform>().rect);
            temp_size = temp_size * canvasScale;
            temp_size.z = 0.1f;
            if((temp_size.x > 0.0f) && (temp_size.y > 0.0f))
            {
                bounds.size = temp_size;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
