using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public GameObject impactEffect;
    public float speed = 70f;

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

    void Start()
    {
        
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        GameObject effectIns = Instantiate<GameObject>(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        Destroy(gameObject);
        Destroy(target.gameObject);
    }
}
