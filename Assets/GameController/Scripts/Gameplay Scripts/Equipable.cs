using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Equipable : MonoBehaviour {


    public enum EquipType
    {
        None = 0,
        SingleHanded =1,
        TwoHanded = 2
    };

    public EquipType TypeOfEquip;
    public GameObject primaryHandEquip;
    public GameObject secondaryHandEquip;
    //public UnityEvent primaryOnUse, secondaryOnUse;

    private bool equipped;
    private GameObject spawnedPrimary, spawnedSecondary;
    //public Valve.VR.EVRButtonId onUseButtonID;

	// Use this for initialization
	void Start () {
		
	}

    public void Equip(GameObject primary, GameObject secondary)
    {
        if(!equipped)
        {
            switch (TypeOfEquip)
            {
                case EquipType.SingleHanded:
                    spawnedPrimary = Instantiate(primaryHandEquip, primary.transform);

                    spawnedPrimary.GetComponent<EquipableController>().inputHandler = primary.GetComponent<ControllerInputHandler>();
                    spawnedPrimary.transform.localPosition = Vector3.zero;
                    break;
                case EquipType.TwoHanded:
                    spawnedPrimary = Instantiate(primaryHandEquip, primary.transform);
                    spawnedSecondary = Instantiate(secondaryHandEquip, secondary.transform);

                    spawnedPrimary.GetComponent<EquipableController>().inputHandler = primary.GetComponent<ControllerInputHandler>();
                    spawnedSecondary.GetComponent<EquipableController>().inputHandler = secondary.GetComponent<ControllerInputHandler>();
                    spawnedPrimary.transform.localPosition = Vector3.zero;
                    spawnedSecondary.transform.localPosition = Vector3.zero;
                    break;
            }
            equipped = true;
        }
        
    }

    public void Unequip()
    {
        if(equipped)
        {
            switch(TypeOfEquip)
            {
                case EquipType.SingleHanded:
                    Destroy(spawnedPrimary);
                    break;
                case EquipType.TwoHanded:
                    Destroy(spawnedPrimary);
                    Destroy(spawnedSecondary);
                    break;
            }

            equipped = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
