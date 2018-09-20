using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGloveController : EquipableController {

    public float fireRate = 0.5f;
    public float bulletSpeed = 200;
    private float fireRateTimer;
    private GameObject spawnedBullet;
	// Use this for initialization
	new void Start () {
        base.Start();

        fireRateTimer = 0.0f;
	}

	void OnDestroy()
    {
        // clear out the object pool for bullets.

    }
	// Update is called once per frame
	void Update () {
        // If the input handler is accepting input
		if(inputHandler.AcceptInput)
        {
            if(inputHandler.TriggerPressed)
            {
                if (fireRateTimer <= 0)
                {
                    if(spawnedBullet == null)
                    {
                        spawnedBullet = GetComponent<GameController.Spawner>().SpawnObj();
                        spawnedBullet.transform.rotation = transform.rotation;
                        spawnedBullet.GetComponent<Rigidbody>().AddForce(spawnedBullet.transform.forward * bulletSpeed, ForceMode.Impulse);
                        fireRateTimer = fireRate;
                    }
                }
                else
                {
                    fireRateTimer = Mathf.Max(0.0f, fireRateTimer - Time.deltaTime);

                    if(spawnedBullet != null)
                    {
                        spawnedBullet = null;
                    }
                }
            }
        }
	}
}
