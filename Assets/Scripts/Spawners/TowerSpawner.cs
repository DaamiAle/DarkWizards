using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    private GameObject currentPlataform;
    [SerializeField] PlayerBehaviour player;
    //[SerializeField] LevelManager levelManager; 
    /*
    public static TowerSpawner Instance { get; private set; }
    void Awake()
    {
        // Verificar si ya hay una instancia existente
        if (Instance == null) Instance = this; // Establecer la instancia actual como la única instancia
        else Destroy(gameObject); // Si ya hay una instancia, destruimos esta
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnTower(int towerType)//Esta es llamada por los botones de la tienda
    {
        GameObject tower=null;
        if (player.Coins >= 100 & towerType == 0)
        {
            tower = gameObject.GetComponent<TowerPool>().CreateTower(towerType, currentPlataform);
            player.Coins -= 100;
        }
        if (player.Coins >= 200 & towerType == 1)
        {
            tower = gameObject.GetComponent<TowerPool>().CreateTower(towerType, currentPlataform);
            player.Coins -=200;
        }
        if (player.Coins >= 300 & towerType == 2)
        {
            tower = gameObject.GetComponent<TowerPool>().CreateTower(towerType, currentPlataform);
            player.Coins -= 300;
        }
        if (player.Coins >= 250 & towerType == 3)
        {
            tower = gameObject.GetComponent<TowerPool>().CreateTower(towerType, currentPlataform);
            player.Coins -= 250;
        }
        if (tower != null)
        {
            tower.SetActive(true);
            currentPlataform.GetComponent<TowerPlataformBehaviour>().SetHasTower(true,tower);
        }
    }
    public void SetCurrentPlataform(GameObject plataform)
    {
        Debug.Log("Cambie de plataforma");
        currentPlataform = plataform;
        //StartCoroutine(SelectedTower());
    }
    private IEnumerator SelectedTower()
    {
        int delayAnim = 10;
        do
        {
            yield return new WaitForSeconds(0.2f);
            delayAnim--;
            Debug.Log($"{delayAnim}");
        }while (delayAnim > 0);

    }

}
