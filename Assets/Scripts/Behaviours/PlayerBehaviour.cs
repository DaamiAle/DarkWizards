using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] int _maxHealth = 100;
    [SerializeField] int _health = 100;
    [SerializeField] int _coins = 200;
    [SerializeField] int _wave = 0;
    [SerializeField] int _timer = 0;
    public int MaxHealth { get => _maxHealth;}
    public int Health { get => _health;}
    public int Coins { get => _coins; set => _coins = value; }
    public int Wave { get => _wave;}
    public int Timer { get => _timer;}
    public bool IsDead { get => _health < 1; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health < 1)
        {
            gameObject.SetActive(false);
        }
    }


    public void TakeDamage(int value)
    {
        if (value > 0)
        {
            _health -= (_health - value < 0) ? _health : value;
        }
    }

}
