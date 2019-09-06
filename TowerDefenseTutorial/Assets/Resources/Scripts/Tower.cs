﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;
    private bool isShoot = false;

    public float range = 15f;
    public float attackSpeed;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private void Start()
    {
        StartCoroutine("CoroutineUpdate");
    }

    private void Update()
    {
        if (target == null)
            return;

        //포탑을 타겟에게로 자연스럽게 돌리는 구문
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        //

        if (!isShoot)
            StartCoroutine("FireBullet");
    }

    IEnumerator CoroutineUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearsetEnemy = null;
        float shortestDis = Mathf.Infinity;

        for(int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if(distanceToEnemy < shortestDis)
            {
                shortestDis = distanceToEnemy;
                nearsetEnemy = enemies[i];
            }

            if(nearsetEnemy != null && shortestDis <= range)
                target = nearsetEnemy.transform;
           else
                target = null;

        }

        yield return new WaitForSeconds(0f);
        StartCoroutine("CoroutineUpdate");
    }

    IEnumerator FireBullet()
    {
        isShoot = true;
        MakeBullet();
        yield return new WaitForSeconds(attackSpeed);
        isShoot = false;

    }

    private void MakeBullet()
    {
        GameObject bulletTemp = Instantiate<GameObject>(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletTemp.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Target = target;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}