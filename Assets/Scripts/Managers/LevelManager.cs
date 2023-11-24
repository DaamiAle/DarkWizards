using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] int LevelNro;
    [SerializeField] Image TransitionImage;
    [SerializeField] GameObject player;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject VictoryModal, DefeatModal;
    private bool isOnTransition, goOut, goIn;
    
    void Awake()
    {
        isOnTransition = false;
        goOut = false;
        goIn = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!gameManager)
        {
            gameManager = GameManager.Instance;
        }
        Invoke("Clarify", 2f);
        //StartCoroutine(DesactiveTransition());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGamePaused)
        {
            List<GameObject> towers = GameObject.FindGameObjectsWithTag("Plataform").ToArray().ToList();
            towers.ForEach(x => x.GetComponent<BoxCollider2D>().enabled = false);
        }
        else
        {
            List<GameObject> towers = GameObject.FindGameObjectsWithTag("Plataform").ToArray().ToList();
            towers.ForEach(x => x.GetComponent<BoxCollider2D>().enabled = true);
        }
        if (isOnTransition && goIn) // Si esta en transicion de entrada
        {
            // Le resta al alpha de la imagen de transicion el tiempo que paso desde el ultimo frame
            TransitionImage.color = new Color(TransitionImage.color.r, TransitionImage.color.g, TransitionImage.color.b, TransitionImage.color.a - Time.deltaTime * 0.5f);
            if (TransitionImage.color.a <= 0f)
            {
                isOnTransition = false; // Ya no esta en transicion
                goIn = false; // Ya no va a entrar
                TransitionImage.gameObject.SetActive(false); // Desactiva la imagen de transicion
            }
        }
        if (isOnTransition && goOut) // Si esta en transicion de salir
        {
            // Si la imagen de transicion no esta activa, la activa
            TransitionImage.gameObject.SetActive(true);
            // Le suma al alpha de la imagen de transicion el tiempo que paso desde el ultimo frame
            TransitionImage.color = new Color(TransitionImage.color.r, TransitionImage.color.g, TransitionImage.color.b, TransitionImage.color.a + Time.deltaTime * 0.5f);
            if (TransitionImage.color.a >= 1f)
            {
                isOnTransition = false; // Ya no esta en transicion
                goOut = false; // Ya no va a salir
            }
        }
        // Condicion de derrota
        if (player.GetComponent<PlayerBehaviour>().IsDead)
        {
            //gameManager.PauseGame();
            //Darken();
            DefeatModal.SetActive(true);
            GoToMainMenu();
        }
        // Condicion de victoria
        else if (enemySpawner.LevelSupered && !player.GetComponent<PlayerBehaviour>().IsDead)
        {
            //gameManager.PauseGame();
            //Darken();
            VictoryModal.SetActive(true);
            Invoke("NextLevel", 4f);
        }
    }

    public int CurrentWave() => enemySpawner.CurrentWave;
    public int TotalWaves() => enemySpawner.WavesCount;
    private void Clarify()
    {
        isOnTransition = true;
        goIn = true;
    }

    private void Darken()
    {
        isOnTransition = true;
        goOut = true;
    }
    /*
    public void InTransition()
    {
        desactiveTransition = true;
    }
    public void OutTransition()
    {
        activeTransition = true;
    }
    public void GoToLevel(int levelNro)
    {
        //OutTransition();
        gameManager.StartLevel(levelNro);
    }
    */
    public void NextLevel()
    {
        gameManager.ResumeGame();
        Darken();
        //OutTransition();
        //gameManager.CompleteLevel(LevelNro);
        if (LevelNro == 5)
        {
            gameManager.GoToMenu();
        }
        else
        {
            gameManager.StartLevel(LevelNro + 1);
        }
    }
    public void GoToMainMenu()
    {
        gameManager.ResumeGame();
        Darken();
        //OutTransition();
        Invoke("GoMenu", 2f);
    }
    public void GoMenu()
    {

        //Darken();
        //OutTransition();
        gameManager.GoToMenu();
    }
}
