using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private float fireRate, speedOfBullet;
    [SerializeField] private Transform firePoints;
    [SerializeField] private GameObject bulletPrefab;
    public GameObject range;
    private Transform target;

    private void Start()
    {
        StartCoroutine(Shoot(fireRate));
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!target && col.tag is "Enemy") target = col.transform;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.transform == target) target = null;
    }

    IEnumerator Shoot(float delay)
    {
        if (target)
        {
            foreach (Transform firePoint in firePoints)
            {
                Rigidbody curBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
                curBullet.AddForce(firePoint.forward * speedOfBullet);
                Destroy(curBullet.gameObject, 10f);
            }
        }
        yield return new WaitForSeconds(delay);
        StartCoroutine(Shoot(fireRate));
    }
}
