using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableController : MonoBehaviour
{
    public ControllerInputHandler inputHandler;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public Vector3 playerPositionOffset;
    public Vector3 playerRotationOffset;

    private bool equipablePlaced;
    private bool wielderTagSet;
    private string wielderTag;

    protected void Start()
    {
        
    }

    protected void Update()
    {
        if(!wielderTagSet)
        {
            if (inputHandler)
            {
                wielderTag = "Player";
            }
            else
            {
                wielderTag = "Enemy";
            }
            wielderTagSet = true;
        }
        
    }

    protected void OnEnable()
    {
        if(inputHandler)
        {
            wielderTag = "Player";
        }
        else
        {
            wielderTag = "Enemy";
        }
    }

    public void PlaceEquipable()
    {
        if (!equipablePlaced)
        {
            if (inputHandler)
            {
                //////Debug.Log(string.Format("Placing equipable at location: {0} with rotation {1}", playerPositionOffset, playerRotationOffset));
                transform.localPosition = playerPositionOffset;
                transform.localEulerAngles = playerRotationOffset;
                
            }
            else
            {
                //////Debug.Log(string.Format("Placing equipable at location: {0} with rotation {1}", playerPositionOffset, playerRotationOffset));
                transform.localPosition = positionOffset;
                transform.localEulerAngles = rotationOffset;
                
            }
            //////Debug.Log(string.Format("Equipable location and rotation after placement: {0}, {1}", transform.localPosition, transform.localEulerAngles));
            equipablePlaced = true;
        }
    }

    public bool EquipablePlaced
    {
        get { return equipablePlaced; }
    }

    public string WielderTag
    {
        get { return wielderTag; }
    }

    public bool WielderTagSet
    {
        get { return wielderTagSet; }
    }

    public bool PrimaryEquipment
    {
        get;
        set;
    }
}

