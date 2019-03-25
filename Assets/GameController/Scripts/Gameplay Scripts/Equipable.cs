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
                    // Instantiate the equipable(s)
                    spawnedPrimary = Instantiate(primaryHandEquip, primary.transform);

                    // Set the input handler of the equipable controller
                    spawnedPrimary.GetComponent<EquipableController>().inputHandler = primary.GetComponent<ControllerInputHandler>();

                    // Zero-out the position of the prefab before placing it.
                    spawnedPrimary.transform.localPosition = Vector3.zero;

                    // Place the equipable in the player's hands based off of the player offset in the equipable
                    spawnedPrimary.GetComponent<EquipableController>().PlaceEquipable();

                    // Flag this equipable as the primary equipable
                    spawnedPrimary.GetComponent<EquipableController>().PrimaryEquipment = true;

                    spawnedPrimary.SetActive(true);

                    GameController.GameController.ActiveGameController.ActivePlayer.equipment.Add(spawnedPrimary.GetComponent<EquipableController>());
                    // Hide the controller model(s)
                    primary.GetComponent<ControllerInputHandler>().ToggleController(false);
                    break;
                case EquipType.TwoHanded:
                    // Instantiate the equipable(s)
                    spawnedPrimary = Instantiate(primaryHandEquip, primary.transform);
                    spawnedSecondary = Instantiate(secondaryHandEquip, secondary.transform);

                    // Set the input handler of the equipable controller
                    spawnedPrimary.GetComponent<EquipableController>().inputHandler = primary.GetComponent<ControllerInputHandler>();
                    spawnedSecondary.GetComponent<EquipableController>().inputHandler = secondary.GetComponent<ControllerInputHandler>();

                    // Zero-out the position of the prefab before placing it.
                    spawnedPrimary.transform.localPosition = Vector3.zero;
                    spawnedSecondary.transform.localPosition = Vector3.zero;

                    // Place the equipable in the player's hands based off of the player offset in the equipable
                    spawnedPrimary.GetComponent<EquipableController>().PlaceEquipable();

                    // Flag this equipable as the primary equipable
                    spawnedPrimary.GetComponent<EquipableController>().PrimaryEquipment = true;

                    // Place the equipable in the player's hands based off of the player offset in the equipable
                    spawnedSecondary.GetComponent<EquipableController>().PlaceEquipable();

                    spawnedPrimary.SetActive(true);
                    spawnedSecondary.SetActive(true);

                    GameController.GameController.ActiveGameController.ActivePlayer.equipment.Add(spawnedPrimary.GetComponent<EquipableController>());
                    GameController.GameController.ActiveGameController.ActivePlayer.equipment.Add(spawnedSecondary.GetComponent<EquipableController>());
                    // Hide the controller model(s)
                    primary.GetComponent<ControllerInputHandler>().ToggleController(false);
                    secondary.GetComponent<ControllerInputHandler>().ToggleController(false);
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
                    spawnedPrimary.GetComponent<EquipableController>().inputHandler.ToggleController(true);

                    Destroy(spawnedPrimary);
                    GameController.GameController.ActiveGameController.ActivePlayer.ClearEquipment();
                    break;
                case EquipType.TwoHanded:
                    spawnedPrimary.GetComponent<EquipableController>().inputHandler.ToggleController(true);
                    spawnedSecondary.GetComponent<EquipableController>().inputHandler.ToggleController(true);

                    Destroy(spawnedPrimary);
                    Destroy(spawnedSecondary);
                    GameController.GameController.ActiveGameController.ActivePlayer.ClearEquipment();
                    break;
            }
            equipped = false;
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
