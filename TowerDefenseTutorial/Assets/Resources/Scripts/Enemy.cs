using System;
using UnityEngine;
using UnityEngine.UI;

//변수 쓰다가 변수 이름 바꿔야 할때는 변수 이름 클릭하고 Ctrl + R + R 하고 변수 이름 바꾸면 그 변수가 쓰인 곳 전부 그 이름으로 바뀜
public class Enemy : MonoBehaviour
{
    public float startSpeed = 15f;
    [HideInInspector]
    public float speed = 15f;

    private float mpHealth;
    public float startHealth;
    [HideInInspector]
    public float health = 100;
    public int moneyValue = 50;
    public GameObject dieEffect;
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;

        health = startHealth;
        mpHealth = 1 / startHealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount -= amount * mpHealth;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        GameObject temp = Instantiate<GameObject>(dieEffect, transform.position, Quaternion.identity);
        Destroy(temp, 4f);
        Destroy(gameObject);

        PlayerStats.money += moneyValue;
        GameManager.enemiesAlive--;
        Debug.Log("Call" + GameManager.enemiesAlive);
    }

    public void Slow(float _slowAmout)
    {
        speed = startSpeed * (1f - _slowAmout);
    }
}
