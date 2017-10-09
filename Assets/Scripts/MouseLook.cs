using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class MouseLook : MonoBehaviour
    {


        public Camera mainCam;
        public float mouseSensitivity = 100.0f;
        public float rollSpeed = 1.0f;
        public float rollSensitivity = 100.0f;
        public float clampAngle = 80.0f;

        private float rotY = 0.0f; // rotation around the up/y axis
        private float rotX = 0.0f; // rotation around the right/x axis
        private float rotZ = 0.0f;

        void Start()
        {
            Vector3 rot = mainCam.transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
            rotZ = rot.z;
        }

        public void Init()
        {
            Vector3 rot = mainCam.transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
            rotZ = rot.z;
        }

        void Update()
        {
#if (UNITY_EDITOR)
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");
            if (Cursor.visible)
            {
                Cursor.visible = false;
            }
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                rotZ += rollSpeed * rollSensitivity * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotZ -= rollSpeed * rollSensitivity * Time.deltaTime;
            }
            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, rotZ);
            mainCam.transform.rotation = localRotation;
#endif

        }
    }
}

