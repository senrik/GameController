using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableSpawner : MonoBehaviour {

    public GameObject equipable;
    public GameObject equipableModel, equipableOutline;
    public GameObject primaryController, secondaryController;
    private GameObject spawnedEquipable;
    private bool equipCheck, unequipCheck;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ControllerInteract"))
        {
            //Stop input while in then trigger
            primaryController = other.GetComponent<ControllerGrabObject>().inputHandler.gameObject;
            primaryController.GetComponent<ControllerInputHandler>().AcceptInput = false;
            secondaryController = primaryController.GetComponent<ControllerInputHandler>().otherController.gameObject;

            secondaryController.GetComponent<ControllerInputHandler>().AcceptInput = false;
            if (!equipableOutline.activeSelf)
            {
                equipableOutline.SetActive(true);
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("ControllerInteract"))
        {

            if (primaryController != null)
            {
                ControllerInputHandler primaryInput = primaryController.GetComponent<ControllerInputHandler>();
                if (primaryInput.TriggerPressed)
                {
                    if (!spawnedEquipable)
                    {
                        if(!equipCheck)
                        {
                            if(!unequipCheck)
                            {
                                spawnedEquipable = Instantiate(equipable, primaryController.transform);
                                equipCheck = true;

                                if (equipableModel.activeSelf)
                                {
                                    equipableModel.SetActive(false);
                                }
                            }
                        }                        
                    }
                    else
                    {
                        if(!equipCheck)
                        {
                            spawnedEquipable.GetComponent<Equipable>().Unequip();
                            Destroy(spawnedEquipable);
                            unequipCheck = true;
                            if(!equipableModel.activeSelf)
                            {
                                equipableModel.SetActive(true);
                            }
                        }
                    }
                }

                if(equipCheck)
                {
                    spawnedEquipable.GetComponent<Equipable>().Equip(primaryController, secondaryController);
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ControllerInteract"))
        {
            primaryController.GetComponent<ControllerInputHandler>().AcceptInput = true;
            secondaryController.GetComponent<ControllerInputHandler>().AcceptInput = true;
            if (primaryController != null)
            {
                primaryController = null;
                secondaryController = null;
            }

            if (equipableOutline.activeSelf)
            {
                equipableOutline.SetActive(false);
            }

            equipCheck = false;
            unequipCheck = false;
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
