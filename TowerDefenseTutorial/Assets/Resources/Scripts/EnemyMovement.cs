using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.35f)
            GetNextWayPoint();

        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        enemy.speed = enemy.startSpeed;//레이저 범위에서 벗어나면 정상적인 이동속도로 돌려주기 위한 구문 1
    }

    private void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

        transform.LookAt(Waypoints.points[wavepointIndex]);
    }

    private void EndPath()
    {
        if (PlayerStats.lives > 0)
            PlayerStats.lives--;
        Destroy(gameObject);
    }
}
