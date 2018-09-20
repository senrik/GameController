using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableController : MonoBehaviour
{
    public ControllerInputHandler inputHandler;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    protected void Start()
    {
        transform.localPosition = positionOffset;
        transform.localEulerAngles = rotationOffset;
    }
}

