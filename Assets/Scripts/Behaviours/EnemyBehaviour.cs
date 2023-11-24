using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] public GameObject Destiny;
    [SerializeField] GameObject lifeBar;
    [SerializeField] int _baseHealth;
    [SerializeField] int _baseDamage;
    [SerializeField] float _baseSpeed;
    [SerializeField] int _reward;
    [SerializeField] Animator animator;
    private int _health;
    private int _damage;
    private float _speed;
    private float _tempSpeed;
    private bool _isPaused = false;
    private bool _isForward = false;

    public int Health { get=>_health; }
    public int Damage { get=>_damage; }
    public float Speed { get=>_speed; }
    public int Reward { get=>_reward; }

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        _health = _baseHealth;
        _damage = _baseDamage;
        _speed = _baseSpeed;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Agent.speed = _baseSpeed;
        if (Destiny != null)
        {
            Agent.SetDestination(Destiny.transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(Destiny.transform.position);

        if (Health < 1)
        {
            Destiny.GetComponent<PlayerBehaviour>().Coins += Reward;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyDestiny"))
        {
            //gameObject.SetActive(false);
            //Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(_damage);
            _health = 0;
        }
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        lifeBar.transform.localScale = new Vector3((float)_health / _baseHealth, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        //lifeBar.UpdateLifeBar(_health);
    }
    /*
    public void Pause()
    {
        _isPaused = true;
        _tempSpeed = _speed;
        _speed = 0;
        animator.enabled = false;
        Agent.isStopped = true;
    }
    public void Unpause()
    {
        _isPaused = false;
        _speed = _tempSpeed;
        animator.enabled = true;
        Agent.isStopped = false;
    }
    public void Forward()
    {
        _isForward = true;
        _tempSpeed = _speed;
        _speed = _tempSpeed * 2;
        animator.speed *= 2;
        Agent.speed *= 2;
    }
    public void Backward()
    {
        _isForward = false;
        _speed = _tempSpeed;
        animator.speed *= 0.5f;
        Agent.speed *= 0.5f;
    }
    */

}
