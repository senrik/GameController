using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class PlayerRig : MonoBehaviour
    {
        public bool DEBUG_MODE = false;
        
        public bool canTeleport;
        public MenuSystem menuSystem;
        public Transform headTransform;
        public Transform cameraRigTransform;
        public List<EquipableController> equipment;
        
        private PlayerInputHandler _input;
        private GameController _gc;
        private GameObject equipable;
        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
            _input = GetComponent<PlayerInputHandler>();

            ToggleVRControllers(true);

            ToggleTeleport(canTeleport);
        }

        public void SetGC(GameController gc)
        {
            _gc = gc;
        }

        public void ToggleEquipment(bool t)
        {
            if(!t)
            {
                foreach(EquipableController e in equipment)
                {
                    if(e != null)
                    {
                        if(e.gameObject.activeSelf)
                        {
                            e.inputHandler.AcceptInput = false;
                            e.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                foreach (EquipableController e in equipment)
                {
                    if (e != null)
                    {
                        if(!e.gameObject.activeSelf)
                        {
                            e.inputHandler.AcceptInput = true;
                            e.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

        public void ClearEquipment()
        {
            equipment.Clear();
        }

        public void ToggleVRControllers(bool t)
        {
            _input.ToggleControllers(t);
        }

        public void ToggleTeleport(bool t)
        {
            _input.ToggleTeleport(t);
        }

        public void ResetPausePressed()
        {
            _input.ResetPausePressed();
        }

        public void SetPlayerPosition(Transform point)
        {
            cameraRigTransform.position = point.position;
        }

        public void Equip(GameObject primary, GameObject secondary)
        {
            equipable.GetComponent<Equipable>().Equip(primary, secondary);
        }

        public void Unequip()
        {
            equipable.GetComponent<Equipable>().Unequip();
            Equipped = false;
        }

        // Update is called once per frame
        void Update()
        {
            switch (GameController.State)
            {
                case GameState.Active:

                    break;
            }
        }

        public bool PausePressed
        {
            get { return _input.MenuButtonPressed; }
        }

        public bool Equipped
        {
            get;
            set;
        }

        public EquipableController PlayerShootingWeapon
        {
            get
            {
                foreach (EquipableController e in equipment)
                {
                    if(e.tag == "ShootingEquipable")
                    {
                        return e;
                    }
                }

                return null;
            }
        }

        public GameObject WeaponEquipable
        {
            get { return equipable; }
            set { equipable = value; }
        }
    }
}

