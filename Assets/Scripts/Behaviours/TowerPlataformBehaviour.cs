using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlataformBehaviour : MonoBehaviour
{
    [SerializeField] private CursorType cursor, defaultCursor;
    [SerializeField] GameObject Shop,TowerMenu;
    private GameObject currentTower;
    private bool hasTower = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (hasTower)
        {
            TowerMenu.SetActive(true);
        }
        else
        {
            gameObject.GetComponentInParent<TowerSpawner>().SetCurrentPlataform(gameObject);
            // si no hay una torreta, abrimos el menu de compra
            Shop.SetActive(true);
        }
    }
    public void SetHasTower(bool value,GameObject tower)
    {
        hasTower = value;
        currentTower = tower;
    }
}
