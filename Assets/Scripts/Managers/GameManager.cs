using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [SerializeField] public bool ComeFromLevel = false;
    public static bool isGamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToMenu()
    {
        ComeFromLevel = false;
        SceneManager.LoadScene(0);

    }

    public void StartLevel(int menuNro)
    {
        ComeFromLevel = true;
        SceneManager.LoadScene(menuNro == 0 ? "Tutorial" : $"Nivel_{menuNro}");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Detiene el tiempo en el juego
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo en el juego
        isGamePaused = false;
    }
    public void AccelerateGame()
    {
        Time.timeScale = 2f;// Duplica la velocidad del juego
    }



    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
