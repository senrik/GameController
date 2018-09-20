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
        public List<Transform> spawnTransforms;
        private float spawnTimer;
        // Use this for initialization
        protected void Start()
        {
            spawnTimer = spawnRate;

            spawnTransforms.Add(transform);
            for (int i = 0; i < objPool.Length; i++)
            {
                objPool[i] = Instantiate(objectPrefab, spawnTransforms[spawnTransforms.Count - 1].position, Quaternion.identity);
            }
        }

        private void OnDestroy()
        {
            for(int i = 0; i < objPool.Length; i++)
            {
                Destroy(objPool[i]);
            }
        }

        public GameObject SpawnObj(int location = 0)
        {
            for(int i = 0; i < objPool.Length; i++)
            {
                if (!objPool[i].activeSelf && objPool[i].GetComponent<SpawnableObj>().Sleeping)
                {
                    objPool[i].SetActive(true);
                    objPool[i].transform.position = spawnTransforms[location].position;
                    return objPool[i];
                }
            }
            return null;
        }

        // Update is called once per frame
        protected void Update()
        {
            //if (spawnTimer >= spawnRate)
            //{
            //    SpawnObj();
            //    spawnTimer = 0.0f;
            //}
            //else
            //{
            //    spawnTimer += Time.deltaTime;
            //}
        }
    }
}

