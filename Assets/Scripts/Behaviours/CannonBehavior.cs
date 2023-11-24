using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehavior : MonoBehaviour
{
    private int _damage;
    private float _range;
    private float _rating;
    private GameObject _target;

    public int Damage { get => _damage; set => _damage = value; }
    public float Range { get => _range; set => _range = value; }
    public float Rating { get => _rating; set => _rating = value; }
    public GameObject Target { get => _target; set => _target = value; }

    [SerializeField] GameObject Base;
    [SerializeField] GameObject Bullet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // si hay un target entonces modificamos la rotacion para que apunte a este
        if (Target != null)
        {
            Vector3 targetPosition = Target.transform.position;
            Vector3 direction = targetPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // Si el tiempo actual es mayor al tiempo del proximo ataque
 
        }
    }
}
