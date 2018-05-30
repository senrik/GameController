using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    public ViveControllerInputTest inputHandler;
    public GameObject laserPrefab;

    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;

    private GameObject reticle;
    private Transform teleportReticleTransform;

    public Transform headTransform;

    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;

    private bool shouldTeleport;

    private GameObject laser;

    private Transform laserTransform;
    private Vector3 hitPoint;
    private Vector3 laserScale;
    private SteamVR_Controller.Device Controller
    {
        get { return inputHandler.Stick; }
    }

    void Awake()
    {
        if(GetComponent<ViveControllerInputTest>())
        {
            inputHandler = GetComponent<ViveControllerInputTest>();
        }
        laserScale = new Vector3();
    }

    private void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

        reticle = Instantiate(teleportReticlePrefab);

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

    private void Teleport()
    {
        shouldTeleport = false;

        Vector3 difference = cameraRigTransform.position - headTransform.position;

        difference.y = 0;

        cameraRigTransform.position = hitPoint + difference;
    }

	// Update is called once per frame
	void Update () {
		if(Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
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
        }
        else
        {
            laser.SetActive(false);

            reticle.SetActive(false);
        }

        if(Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            Teleport();
        }
	}
}
