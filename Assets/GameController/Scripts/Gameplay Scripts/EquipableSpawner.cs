using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableSpawner : MonoBehaviour {

    public GameObject pickUpText;
    public GameObject equipable;
    public GameObject equipableModel, equipableOutline;
    public GameObject primaryController, secondaryController;
    private GameObject spawnedEquipable;
    private GameController.PlayerRig m_player;
    private bool equipCheck, unequipCheck;
	// Use this for initialization
	void Start () {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController.PlayerRig>();
	}

    private void OnEnable()
    {
        // Show the equipable model
        if (!equipableModel.activeSelf)
        {
            equipableModel.SetActive(true);
        }

        // Show the pickup text
        if (pickUpText.activeSelf)
        {
            pickUpText.SetActive(true);
        }
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

            primaryController.GetComponent<ControllerInputHandler>().Stick.TriggerHapticPulse();

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
            // If the primary controller is not null
            if (primaryController != null)
            {
                // Get the primary input handler
                ControllerInputHandler primaryInput = primaryController.GetComponent<ControllerInputHandler>();

                // If the trigger is pressed
                if (primaryInput.TriggerPressed)
                {
                    // if the equipable has not spawned (null)
                    if (!spawnedEquipable)
                    {
                        // If we have not equipped any thing while our hands are in the area
                        if(!equipCheck)
                        {
                            // If we have not unequipped any thing while our hands are in the area
                            if(!unequipCheck)
                            {
                                // Instantiate a new equippable
                                spawnedEquipable = Instantiate(equipable, primaryController.transform);

                                m_player.WeaponEquipable = spawnedEquipable;

                                // Flag us as having equipped something
                                equipCheck = true;

                                // Hide the equipable model
                                if (equipableModel.activeSelf)
                                {
                                    equipableModel.SetActive(false);
                                }

                                // Hide the pickup text
                                if(pickUpText.activeSelf)
                                {
                                    pickUpText.SetActive(false);
                                }
                            }
                        }                        
                    }
                    // If the equipable is spawned (not null)
                    else
                    {
                        // If we have not equipped any thing
                        if(!equipCheck)
                        {
                            // Unequip the equippabled
                            //spawnedEquipable.GetComponent<Equipable>().Unequip();
                            m_player.Unequip();

                            // Destroy the spawned equippable (becomes null)
                            Destroy(spawnedEquipable);

                            // Flagged as having unequipped some thing
                            unequipCheck = true;

                            // Show the equipable again
                            if(!equipableModel.activeSelf)
                            {
                                equipableModel.SetActive(true);
                            }

                            // Show the pickup text again
                            if (!pickUpText.activeSelf)
                            {
                                pickUpText.SetActive(true);
                            }
                        }
                    }
                }
                // If we have instantiated an equipable, and flagged it as such
                if(equipCheck)
                {
                    // equip the equipable using the primary and secondary controllers
                    //spawnedEquipable.GetComponent<Equipable>().Equip(primaryController, secondaryController);
                    m_player.Equip(primaryController, secondaryController);
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ControllerInteract"))
        {
            // If the primary controller is not null
            if (primaryController != null)
            {
                // Allow the controllers to accept input when they leave the equipable area
                primaryController.GetComponent<ControllerInputHandler>().AcceptInput = true;
                secondaryController.GetComponent<ControllerInputHandler>().AcceptInput = true;

                // Make the references to the primary and secondary controllers null
                primaryController = null;
                secondaryController = null;
            }

            // Hide the equipable outline
            if (equipableOutline.activeSelf)
            {
                equipableOutline.SetActive(false);
            }

            // Reset the equip and unequip flags
            equipCheck = false;
            unequipCheck = false;
            // If a spawnedEquipable exists
            if(spawnedEquipable)
            {
                // Flag the player as being equipped
                m_player.Equipped = true;
            }
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
