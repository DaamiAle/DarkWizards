using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{

    [SerializeField] int _baseDamage;
    [SerializeField] float _baseRange;
    [SerializeField] float _baseRating;
    [SerializeField] int _cost;
    [SerializeField] int _costUpgrade;
    private int _damage;
    private float _range;
    private float _rating;
    private float _tempRating;
    private int _level;

    [SerializeField] GameObject Base;
    [SerializeField] GameObject Cannon;
    [SerializeField] GameObject Range;
    [SerializeField] GameObject Bullet;
    [SerializeField] CursorType defaultCursor, handCursor;
    [SerializeField] Animator animator;
    private GameObject plataform;
    private Transform target = null;
    private float nextTimeToAttack = 0;
    private bool _isPaused = false;
    private bool _isForward = false;


    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Se setea el tiempo del pr�ximo ataque
        nextTimeToAttack = Time.time;
        // Obtengo el animator del ca�on
        if (animator == null) animator = Cannon.GetComponent<Animator>();
        _damage = _baseDamage;
        _range = _baseRange;
        _rating = _baseRating;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.gameObject.GetComponent<EnemyBehaviour>().Health <= 0)
        {
            target = null;
            Bullet.GetComponent<BulletBehaviour>().SetTarget(null);
        }
        // Si no hay un objetivo
        if (target == null)
        {
            Bullet.SetActive(false);
            // Desactivamos la animaci�n de disparo
            animator.SetBool("isShooted", false);
            Bullet.transform.position = Cannon.transform.position;

            DetectEnemies();
        }
        else
        {
            Cannon.transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
        }
        // Comprobamos si ya pas� el tiempo de ataque
        if (Time.time >= nextTimeToAttack)
        {
            // Se setea el tiempo del pr�ximo ataque
            nextTimeToAttack = Time.time + _rating;
            // Atacamos
            Attack();
        }
        Range.transform.localScale = new Vector3(_range * 2, _range * 2, 1);
    }
    void Attack()
    {
        // Si ya pas� el tiempo de ataque
        if (target != null && !Bullet.activeInHierarchy)
        {
            // Activamos la animaci�n de disparo
            animator.SetBool("isShooted", true);
            // Modificamos la velocidad de la animacion de disparo
            //animator.speed = attackDelay;
            // Se setea la posici�n de la bala en la posici�n de la torreta
            Bullet.transform.position = Cannon.transform.position;
            // Se setea el da�o de la bala
            Bullet.GetComponent<BulletBehaviour>().SetDamage(_damage);
            // Se setea el objetivo de la bala
            Bullet.GetComponent<BulletBehaviour>().SetTarget(target);
            // Se setea la velocidad de la bala
            Bullet.GetComponent<BulletBehaviour>().SetSpeed(10f);
            // Se activa la bala, lo que hace que se mueva y haga da�o si la animacion termin�
            Bullet.SetActive(true);
        }
    }
    void DetectEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        List<GameObject> enemiesInRange = new();
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            //Debug.Log("Distancia: " + distance);
            if (distance <= _range)
            {
                //Debug.Log("Encontre un enemigo cerca");
                enemiesInRange.Add(enemy);
            }
        }
        if (enemiesInRange.Count > 0)
        {
            //Debug.Log("Tengo un enemigo a tiro");
            target = enemiesInRange[0].transform;
            // Rotamos el ca�on para que apunte al enemigo

        }
        else
        {
            //Debug.Log("No tengo ningun enemigo a tiro");
            target = null;
            Bullet.GetComponent<BulletBehaviour>().SetTarget(null);
        }
    }

    private void OnMouseEnter()
    {
        Range.SetActive(true);
        // cambiamos el cursor de juego a hand
        Cursor.SetCursor(handCursor.cursorTexture, handCursor.cursorHotspot, CursorMode.ForceSoftware);
    }
    private void OnMouseExit()
    {
        Range.SetActive(false);
        Cursor.SetCursor(defaultCursor.cursorTexture, defaultCursor.cursorHotspot, CursorMode.Auto);
    }



    /*
    public void Pause()
    {
        // Pausamos la animacion
        animator.enabled = false;
        // Pausamos el disparo
        enabled = false;
        _tempRating = _rating;
        _rating = 0;
    }

    public void Unpause()
    {
        // Reanudamos la animacion
        animator.enabled = true;
        // Reanudamos el disparo
        _rating = _tempRating;
    }

    public void Forward()
    {
        // Aumentamos la velocidad de la animacion
        animator.speed *= 2;
        // Aumentamos la velocidad de disparo
        _tempRating = _rating;
        _rating = _rating * 2;
    }
    public void Backward()
    {
        // Disminuimos la velocidad de la animacion
        animator.speed *= 0.5f;
    }
    */
}
