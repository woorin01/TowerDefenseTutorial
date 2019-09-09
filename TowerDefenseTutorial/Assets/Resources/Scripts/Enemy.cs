using System;
using UnityEngine;

//변수 쓰다가 변수 이름 바꿔야 할때는 변수 이름 클릭하고 Ctrl + R + R 하고 변수 이름 바꾸면 그 변수가 쓰인 곳 전부 그 이름으로 바뀜
public class Enemy : MonoBehaviour
{
    public float startSpeed = 15f;
    [HideInInspector]
    public float speed = 15f;

    public float health = 100;
    public int moneyValue = 50;
    public GameObject dieEffect;

    private void Start()
    {
        speed = startSpeed;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        GameObject temp = Instantiate<GameObject>(dieEffect, transform.position, Quaternion.identity);
        Destroy(temp, 4f);
        Destroy(gameObject);

        PlayerStats.money += moneyValue;
    }

    public void Slow(float _slowAmout)
    {
        speed = startSpeed * (1f - _slowAmout);
    }
}
