using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class Spawner : MonoBehaviour
    {

        public GameObject objectPrefab;
        public float spawnRate = 0.1f;
        private GameObject[] objPool = new GameObject[30];
        private Transform spawnTransform;
        private float spawnTimer;
        // Use this for initialization
        void Start()
        {
            spawnTimer = spawnRate;
            spawnTransform = transform;
            for (int i = 0; i < objPool.Length; i++)
            {
                objPool[i] = Instantiate(objectPrefab, spawnTransform.position, Quaternion.identity);
            }
        }

        void SpawnObj()
        {
            for(int i = 0; i < objPool.Length; i++)
            {
                if (!objPool[i].activeSelf)
                {
                    objPool[i].SetActive(true);
                    break;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (spawnTimer >= spawnRate)
            {
                SpawnObj();
                spawnTimer = 0.0f;
            }
            else
            {
                spawnTimer += Time.deltaTime;
            }
        }
    }
}

