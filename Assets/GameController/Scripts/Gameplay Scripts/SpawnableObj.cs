using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class SpawnableObj : MonoBehaviour
    {

        public float lifespan = 2;
        private Vector3 initPos = Vector3.zero;

        IEnumerator ObjLife()
        {
            yield return new WaitForSeconds(lifespan);
            ToggleObj(false);
            Debug.Log("Spawnable lifespan end.");
        }

        private void OnEnable()
        {
            StartCoroutine(ObjLife());
            Debug.Log("Spawnable lifespan start.");
            if(initPos == Vector3.zero)
            {
                initPos = transform.position;
            }
        }

        public void ToggleObj(bool t)
        {
            if (t)
            {
                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                if (gameObject.activeSelf)
                {
                    GetComponent<Rigidbody>().Sleep();
                    transform.position = initPos;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

