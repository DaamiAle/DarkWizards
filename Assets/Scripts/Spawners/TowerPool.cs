using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPool : MonoBehaviour
{

    [SerializeField] List<GameObject> objectsPrefabs;

    public static TowerPool Instance { get; private set; }

    private List<GameObject> towerPool;

    void Awake()
    {
        // Verificar si ya hay una instancia existente
        if (Instance == null) Instance = this; // Establecer la instancia actual como la única instancia
        else Destroy(gameObject); // Si ya hay una instancia, destruimos esta
    }
    // Start is called before the first frame update
    void Start()
    {
        towerPool = new List<GameObject>(); // Inicializar el pool de torres
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Obtener una torre del pool
    public GameObject CreateTower(int indexTowerType, GameObject plataform)
    {
        GameObject towerReturn = Instantiate(objectsPrefabs[indexTowerType], plataform.transform.position, plataform.transform.rotation);
        towerPool.Add(towerReturn);
        return towerReturn;
    }
}
