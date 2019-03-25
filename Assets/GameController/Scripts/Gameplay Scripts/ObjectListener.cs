using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class ObjectListener : MonoBehaviour
    {


        public GameObject listenObj;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (listenObj)
            {
                
                if(GameController.State == GameState.Active)
                {
                    if (!listenObj.activeSelf)
                    {
                        listenObj.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }
            //Debug.Log("Object listener update");
        }

    }
}
