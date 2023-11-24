using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TowerBehaviour_2 : MonoBehaviour
{
    [SerializeField] int _baseDamage;
    [SerializeField] float _baseRange;
    [SerializeField] float _baseRating;
    [SerializeField] int _cost;
    [SerializeField] int _costUpgrade;
    private int _damage;
    private float _range;
    private float _rating;

    public int Damage { get => _damage; set => _damage = value; }
    public float Range { get => _range; set => _range = value; }
    public float Rating { get => _rating; set => _rating = value; }
    public int Cost { get => _cost; set => _cost = value; }
    public int CostUpgrade { get => _costUpgrade; set => _costUpgrade = value; }

    //[SerializeField] GameObject Plataform;
    [SerializeField] GameObject Cannon;
    [SerializeField] GameObject RangeObject;
    [SerializeField] GameObject Destiny;
    //[SerializeField] CursorType defaultCursor, handCursor;
    //[SerializeField] Animator animator;
    private Transform target = null;

    void Awake()
    {
        
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (target != null && target.gameObject.GetComponent<EnemyBehaviour>().Health <= 0)
        {
            target = null;
        }
        // Si no hay un objetivo
        if (target == null)
        {
            List<GameObject> enemies = EnemiesInRange();
            // Se busca un objetivo
            Cannon.GetComponent<CannonBehavior>().Target = enemies.Count > 0 ? FindTarget(enemies) : null;
        }
    }

    private List<GameObject> EnemiesInRange()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (GameObject enemy in allEnemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= _range)
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }
    private GameObject FindTarget(List<GameObject> enemies)
    {
        //List<GameObject> enemies = EnemiesInRange();
        //NavMeshPath path = new NavMeshPath();
        //GameObject target = null;

        NavMeshPath path = new NavMeshPath();
        return enemies.OrderBy(enemy =>
        {
            NavMesh.CalculatePath(enemy.transform.position, enemy.GetComponent<EnemyBehaviour>().Destiny.transform.position, NavMesh.GetAreaFromName("Walkable"), path);
            return path.status == NavMeshPathStatus.PathComplete ? path.corners.Length : int.MaxValue;
        }).First();

    }
}
