using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class SpawnableObj : MonoBehaviour
    {

        public float lifespan = 2;
        public float restTime = 5;
        public GameObject rootObj;
        private Vector3 initPos = Vector3.zero;
        private bool sleep = true;

        IEnumerator ObjLife()
        {
            yield return new WaitForSeconds(lifespan);
            ToggleObj(false);
            Debug.Log("Spawnable lifespan end.");
        }

        IEnumerator PutToSleep()
        {
            yield return new WaitForSeconds(restTime);
            sleep = true;
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            StartCoroutine(ObjLife());
            Debug.Log("Spawnable lifespan start.");
            if(initPos == Vector3.zero)
            {
                initPos = transform.position;
            }
            ToggleObj(true);
        }

        public void ToggleObj(bool t)
        {
            if (t)
            {
                if (!rootObj.activeSelf)
                {
                    rootObj.SetActive(true);
                    GetComponent<Rigidbody>().WakeUp();
                    sleep = false;
                }
            }
            else
            {
                if (rootObj.activeSelf)
                {
                    GetComponent<Rigidbody>().Sleep();
                    transform.position = initPos;
                    rootObj.SetActive(false);
                    StartCoroutine(PutToSleep());
                }
            }
        }

        public bool Sleeping
        {
            get { return sleep; }
            set { sleep = value; }
        }
    }
}

