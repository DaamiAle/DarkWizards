using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private int damage;
    private Transform target;
    private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (target != null && gameObject.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, bulletSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyUnit"))
        {
            gameObject.SetActive(false);
            collision.GetComponent<EnemyBehaviour>().TakeDamage(damage);
        }
    }
    public void SetDamage(int value) => damage = value;
    public void SetTarget(Transform value)
    {
        target = value;
        if (target == null)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetSpeed(float value) => bulletSpeed = value;
}
