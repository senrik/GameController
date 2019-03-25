using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class LaserPointer : MonoBehaviour
    {

        public ControllerInputHandler inputHandler;
        public GameObject laserPrefab;
        public GameObject uiPointerPrefab;

        public bool allowTeleport;

        public Transform cameraRigTransform;
        public GameObject teleportReticlePrefab;
        public GameObject uiLaserReticlePrefab;

        private GameObject reticle, uiReticle;
        private Transform teleportReticleTransform, uiReticleTransform;

        public Transform headTransform;

        public Vector3 teleportReticleOffset;
        public LayerMask teleportMask;
        public LayerMask uiMask;

        private bool shouldTeleport, uiElement;

        private GameObject laser, uiLaser;
        private UIInteractiveElement selectedUIElement;
        private Transform laserTransform, uiLaserTransform;
        private Vector3 hitPoint;
        private Vector3 laserScale;
        private SteamVR_Controller.Device Controller
        {
            get { return inputHandler.Stick; }
        }


        void Awake()
        {
            if (GetComponent<ControllerInputHandler>())
            {
                inputHandler = GetComponent<ControllerInputHandler>();
            }
            laserScale = new Vector3();
        }

        private void Start()
        {
            laser = Instantiate(laserPrefab, transform);
            uiLaser = Instantiate(uiPointerPrefab, transform);

            laserTransform = laser.transform;
            uiLaserTransform = uiLaser.transform;

            reticle = Instantiate(teleportReticlePrefab, transform);
            uiReticle = Instantiate(uiLaserReticlePrefab, transform);

            teleportReticleTransform = reticle.transform;
            uiReticleTransform = uiReticle.transform;
        }

        private void ShowLaser(RaycastHit hit)
        {
            laser.SetActive(true);
            laserTransform.position = Vector3.Lerp(transform.position, hitPoint, 0.5f);

            laserTransform.LookAt(hitPoint);

            laserScale = laserTransform.localScale;
            laserScale.z = hit.distance;

            laserTransform.localScale = laserScale;
        }

        private void ShowUILaser(RaycastHit hit)
        {
            if (laser.activeSelf)
            {
                laser.SetActive(false);
            }
            uiLaser.SetActive(true);
            uiLaserTransform.position = Vector3.Lerp(transform.position, hitPoint, 0.5f);

            uiLaserTransform.LookAt(hitPoint);

            laserScale = uiLaserTransform.localScale;
            laserScale.z = hit.distance;

            uiLaserTransform.localScale = laserScale;
        }

        private void Teleport()
        {
            shouldTeleport = false;

            Vector3 difference = cameraRigTransform.position - headTransform.position;

            difference.y = 0;

            cameraRigTransform.position = hitPoint + difference;
        }

        private void SelectUIElement()
        {
            if (selectedUIElement != null)
            {
                ////Debug.Log(selectedUIElement.name + " selected.");
                if (selectedUIElement.GetType() == typeof(UIButtonElement))
                {
                    ((UIButtonElement)selectedUIElement).OnHoverEnd();
                }
                else
                {
                    selectedUIElement.OnHoverEnd();
                }
                
                selectedUIElement.OnInteract.Invoke();
                selectedUIElement = null;
            }

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            switch (GameController.State)
            {
                case GameState.Active:
                    #region Active
                    
                    if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        
                        if (allowTeleport)
                        {
                            if (Physics.Raycast(transform.position, transform.forward, out hit, 100, teleportMask))
                            {
                                hitPoint = hit.point;
                                ShowLaser(hit);

                                reticle.SetActive(true);

                                teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                                teleportReticleTransform.forward = hit.normal;

                                shouldTeleport = true;
                            }
                        }
                        
                        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, uiMask))
                        {
                            hitPoint = hit.point;
                            ShowUILaser(hit);
                            
                            selectedUIElement = (hit.collider.GetComponent<ButtonBounds>()) ? hit.collider.GetComponent<ButtonBounds>().InteractableElement : null;
                            if (selectedUIElement != null)
                            {
                                if(selectedUIElement.GetType() == typeof(UIButtonElement))
                                {
                                    ((UIButtonElement)selectedUIElement).OnHoverStart();
                                }
                                else
                                {
                                    selectedUIElement.OnHoverStart();
                                }
                                
                            }
                            uiElement = true;
                        }
                        else
                        {
                            uiLaser.SetActive(false);
                            if(selectedUIElement != null)
                            {
                                if (selectedUIElement.GetType() == typeof(UIButtonElement))
                                {
                                    ((UIButtonElement)selectedUIElement).OnHoverEnd();
                                    
                                }
                                else
                                {
                                    selectedUIElement.OnHoverEnd();
                                    
                                }

                                selectedUIElement = null;
                            }
                            

                            
                        }
                    }
                    else
                    {
                        laser.SetActive(false);
                        uiLaser.SetActive(false);
                        reticle.SetActive(false);
                    }

                    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
                    {
                        Teleport();
                    }
                    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && uiElement)
                    {
                        SelectUIElement();
                    }


                    if (Physics.Raycast(transform.position, transform.forward, out hit, 100, uiMask))
                    {
                        hitPoint = hit.point;
                        // place the ui reitcle at the hit point, and have it offset so that is it placed in front of the score panel.
                        uiReticle.SetActive(true);
                        uiReticleTransform.position = hitPoint + new Vector3(0,0, 0.025f);
                        uiReticleTransform.rotation = Quaternion.identity;
                    }
                    else
                    {
                        //reset the UI reticle
                        uiReticle.SetActive(false);
                    }

                    #endregion
                    break;
                case GameState.Paused:
                    #region Paused
                    if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        
                        
                        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, uiMask))
                        {
                            hitPoint = hit.point;
                            ShowUILaser(hit);
                            selectedUIElement = hit.collider.GetComponent<ButtonBounds>().InteractableElement;
                            if (selectedUIElement != null)
                            {
                                selectedUIElement.OnHoverStart();
                            }
                            uiElement = true;
                        }
                        else
                        {
                            uiLaser.SetActive(false);
                            if (selectedUIElement != null)
                            {
                                selectedUIElement.OnHoverEnd();
                            }

                            selectedUIElement = null;
                        }
                    }
                    else
                    {
                        laser.SetActive(false);
                        uiLaser.SetActive(false);
                        reticle.SetActive(false);
                    }
                    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && uiElement)
                    {
                        SelectUIElement();
                    }
                    #endregion
                    break;
                case GameState.Loading:
                    #region Loading
                    #endregion
                    break;
            }
            
        }
    }

}
