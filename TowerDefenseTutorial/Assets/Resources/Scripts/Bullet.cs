using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public float speed = 70f;
    public int attackDamage = 50;

    public Transform Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }//get, set

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;//거리 = 속력 * 시간;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //if (Vector3.Distance(target.position, transform.position) <= 0.3f)
        //{
        //    HitTarget();
        //    return;
        //}

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        //transform.position += dir.normalized * distanceThisFrame;
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        GameObject effectIns = Instantiate<GameObject>(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);// (중점, 반지름) 중점에서 반지름만큼 구를 그려 그 안에 있는 콜라이더를 가진 객체를 콜라이더 배열형태로 반환해준다

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                Damage(colliders[i].transform);
            }
        }
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
            e.TakeDamage(attackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
