using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class UIPointer : MonoBehaviour
    {

        public GameObject pointerModelPrefab;

        private GameObject pointerModel;
        private Vector3 hitPoint;
        private SteamVR_TrackedObject trackedObj;
        private SteamVR_Controller.Device Controller
        {
            get { return SteamVR_Controller.Input((int)trackedObj.index); }
        }

        private void Awake()
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }

        // Use this for initialization
        void Start()
        {
            pointerModel = Instantiate(pointerModelPrefab);
        }

        void ShowPointer(RaycastHit hit)
        {
            pointerModel.SetActive(true);

            pointerModel.transform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, 0.5f);

            pointerModel.transform.LookAt(hitPoint);

            pointerModel.transform.localScale = new Vector3(pointerModel.transform.localScale.x, pointerModel.transform.localScale.y, hit.distance /*Mathf.Infinity*/);
        }

        // Update is called once per frame
        void Update()
        {
            /* If this controller's hair trigger is being pressed. */
            if(Controller.GetHairTriggerDown())
            {
                /* Cast a ray with a UI layer-mask. */
                RaycastHit hit;

                if(Physics.Raycast(trackedObj.transform.position, transform.forward, out hit))
                {
                    hitPoint = hit.point;
                    ShowPointer(hit);
                }
                
            }
        }
    }
}

