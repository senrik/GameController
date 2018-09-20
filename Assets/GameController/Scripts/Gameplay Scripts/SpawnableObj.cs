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
        public bool isPhysicsObj;
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

        IEnumerator ToggleTimer(bool t, float toggleTime)
        {
            yield return new WaitForSeconds(toggleTime);
            if (t)
            {
                if (!rootObj.activeSelf)
                {
                    rootObj.SetActive(true);
                    if(isPhysicsObj)
                    {
                        if (rootObj.GetComponent<Rigidbody>())
                        {
                            rootObj.GetComponent<Rigidbody>().WakeUp();
                            rootObj.GetComponent<Rigidbody>().isKinematic = false;
                        }
                    }

                    sleep = false;
                }
            }
            else
            {
                if (rootObj.activeSelf)
                {
                    if (isPhysicsObj)
                    {
                        if (rootObj.GetComponent<Rigidbody>())
                        {
                            rootObj.GetComponent<Rigidbody>().Sleep();
                            rootObj.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                    transform.position = initPos;
                    rootObj.SetActive(false);
                    StartCoroutine(PutToSleep());
                }
            }
        }

        public void ToggleObj(bool t, float toggleTime = 0)
        {
            sleep = !t;
            StartCoroutine(ToggleTimer(t, toggleTime));
        }

        public bool Sleeping
        {
            get { return sleep; }
            set { sleep = value; }
        }
    }
}

