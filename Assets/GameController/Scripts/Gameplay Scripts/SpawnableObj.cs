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
        protected Vector3 initPos = Vector3.zero;
        private bool sleep = true;

        protected IEnumerator ObjLife()
        {
            yield return new WaitForSeconds(lifespan);
            ToggleObj(false);
            ////Debug.Log("Spawnable lifespan end.");
        }

        IEnumerator PutToSleep()
        {
            yield return new WaitForSeconds(restTime);
            gameObject.SetActive(false);
            //ToggleObj(false);
        }

        protected IEnumerator ToggleTimer(bool t, float toggleTime)
        {
            yield return new WaitForSeconds(toggleTime);
            if (t)
            {
                if (!rootObj.activeSelf)
                {
                    if (isPhysicsObj)
                    {
                        rootObj.SetActive(true);
                        if (isPhysicsObj)
                        {
                            if (rootObj.GetComponent<Rigidbody>())
                            {
                                rootObj.GetComponent<Rigidbody>().WakeUp();
                                rootObj.GetComponent<Rigidbody>().isKinematic = false;
                            }
                        }

                        sleep = false;
                    }

                    rootObj.SetActive(true);
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

