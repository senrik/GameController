using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    /// <summary>
    /// Prevents the object that this script is attached to from updating while the game state is not active. Should be placed on spawners, and controllers.
    /// </summary>
    public class StateListener : MonoBehaviour
    {
        public GameObject listenerPrefab;
        
        // Update is called once per frame
        void Update()
        {
            if(GameController.State != GameState.Active)
            {
                if (gameObject.activeSelf)
                {
                    //Instantiate listener as child and have it wait until the state changes.
                    GameObject listen = Instantiate(listenerPrefab);
                    listen.GetComponent<ObjectListener>().listenObj = gameObject;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

