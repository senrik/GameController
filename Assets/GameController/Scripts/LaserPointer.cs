using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class LaserPointer : MonoBehaviour
    {

        public ViveControllerInputTest inputHandler;
        public GameObject laserPrefab;
        public GameObject uiPointerPrefab;

        public Transform cameraRigTransform;
        public GameObject teleportReticlePrefab;

        private GameObject reticle;
        private Transform teleportReticleTransform;

        public Transform headTransform;

        public Vector3 teleportReticleOffset;
        public LayerMask teleportMask;
        public LayerMask uiMask;

        private bool shouldTeleport, uiElement;

        private GameObject laser, uiLaser;
        private UIInterativeElement selectedUIElement;
        private Transform laserTransform, uiLaserTransform;
        private Vector3 hitPoint;
        private Vector3 laserScale;
        private SteamVR_Controller.Device Controller
        {
            get { return inputHandler.Stick; }
        }


        void Awake()
        {
            if (GetComponent<ViveControllerInputTest>())
            {
                inputHandler = GetComponent<ViveControllerInputTest>();
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

            teleportReticleTransform = reticle.transform;
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
                Debug.Log(selectedUIElement.name + " selected.");
                selectedUIElement.OnHoverEnd();
                selectedUIElement.OnInteract.Invoke();
                selectedUIElement = null;
            }

        }

        // Update is called once per frame
        void Update()
        {
            switch (GameController.State)
            {
                case GameState.Active:
                    #region Active
                    if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, teleportMask))
                        {
                            hitPoint = hit.point;
                            ShowLaser(hit);

                            reticle.SetActive(true);

                            teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                            shouldTeleport = true;
                        }
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

                    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
                    {
                        Teleport();
                    }
                    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && uiElement)
                    {
                        SelectUIElement();
                    }
                    #endregion
                    break;
                case GameState.Paused:
                    #region Paused
                    if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        RaycastHit hit;
                        
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
