using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;
    public int towerDamage;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float attackSpeed;

    [Header("Use Laser")]
    public bool useLaser = false;
    public float slowAmount = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpactEffect;
    public Light laserImpactLight;

    [Header("Unity Setup Fields")]
    public Transform firePoint;

    private Transform target;
    private Enemy targetEnemy;
    private bool isShoot = false;

    private void Start()
    {
        StartCoroutine("CoroutineUpdate");
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpactLight.enabled = false;
                    laserImpactEffect.Stop();
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (!isShoot)
                StartCoroutine("FireBullet");
        }
    }

    private void LockOnTarget()
    {
        //포탑을 타겟에게로 자연스럽게 돌리는 구문
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        //transform.LookAt(target);

    }

    IEnumerator lerp()//보간을 이런식으로 사용하면 startPos가 어떻던 객체가 endPos로 도달하기 까지의 시간이 같다
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(100f, 0, 100f);

        for (float i = 0f; i <= 1f; i += Time.deltaTime)// or 0.01f or 아무 수
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(towerDamage * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserImpactLight.enabled = true;
            laserImpactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        laserImpactEffect.transform.position = target.position + dir.normalized;
        //laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        laserImpactEffect.transform.LookAt(firePoint);
    }

    IEnumerator CoroutineUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearsetEnemy = null;
        float shortestDis = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distanceToEnemy < shortestDis)
            {
                shortestDis = distanceToEnemy;
                nearsetEnemy = enemies[i];
            }
        }
        //if (target != null)
        //    if (target != nearsetEnemy)
        //        target.GetComponent<Enemy>().speed = target.GetComponent<Enemy>().startSpeed; //레이저 범위에서 벗어나면 정상적인 이동속도로 돌려주기 위한 구문 2
        if (nearsetEnemy != null && shortestDis <= range)
        {
            target = nearsetEnemy.transform;
            targetEnemy = nearsetEnemy.GetComponent<Enemy>();
        }
        else
        {
            //if(target != null)
            //    target.GetComponent<Enemy>().speed = target.GetComponent<Enemy>().startSpeed; //레이저 범위에서 벗어나면 정상적인 이동속도로 돌려주기 위한 구문 2
            target = null;
        }

        yield return new WaitForSeconds(0.1f);
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

        if (bullet != null)
        {
            bullet.Target = target;
            bullet.attackDamage = towerDamage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
