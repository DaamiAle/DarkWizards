using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject PlayButton, PauseButton, ForwardButton;
    [SerializeField] GameObject GameMenu, Shop, TowerMenu, ExitConfirm;
    [SerializeField] TextMeshProUGUI HealthText, MoneyText, WaveText, TimerText;
    [SerializeField] PlayerBehaviour player;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] Image HealthBar, TransitionImage;
    [SerializeField] LevelManager levelManager;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameMenu != null)
        {
            HealthText.text = $"{player.Health} / {player.MaxHealth}";
            HealthBar.fillAmount = (float)player.Health / (float)player.MaxHealth;
            MoneyText.text = $"{player.Coins}";
            WaveText.text = $"{levelManager.CurrentWave()} / {levelManager.TotalWaves()}";
        }

    }

    public void ActiveVictory()
    {

    }
    public void ActiveDefeat()
    {

    }

    #region ButtonsControllers
    // Es llamada al presionar el boton de menu
    public void OpenMenu()
    {
        PlayButton.GetComponent<Button>().interactable = false;
        PauseButton.GetComponent<Button>().interactable = false;
        ForwardButton.GetComponent<Button>().interactable = false;
        GameManager.Instance.PauseGame();
        ShowGameMenu();
    }
    // Es llamada al cerrar el menu
    public void CloseMenu()
    {
        PlayButton.GetComponent<Button>().interactable = true;
        PauseButton.GetComponent<Button>().interactable = true;
        ForwardButton.GetComponent<Button>().interactable = true;
        GameManager.Instance.ResumeGame();
    }
    // Es llamada al presionar el boton de play
    public void UnpauseGame()
    {
        PlayButton.GetComponent<Button>().interactable = false;
        PauseButton.GetComponent<Button>().interactable = true;
        ForwardButton.GetComponent<Button>().interactable = true;
        GameManager.Instance.ResumeGame();
    }
    // Es llamada al presionar el boton de pausa
    public void PauseGame()
    {
        PauseButton.GetComponent<Button>().interactable = false;
        PlayButton.GetComponent<Button>().interactable = true;
        GameManager.Instance.PauseGame();
        ForwardButton.GetComponent<Button>().interactable = true;
    }
    // Es llamada al presionar el boton de forward
    public void ForwardGame()
    {
        GameManager.Instance.AccelerateGame();
        ForwardButton.GetComponent<Button>().interactable = false;
        PlayButton.GetComponent<Button>().interactable = true;
    }
    
    #endregion

    #region ModalsControllers
    public void ShowGameMenu()
    {
        Shop.SetActive(false);
        TowerMenu.SetActive(false);
        GameMenu.SetActive(true);
    }
    public void ShowShop()
    {
        Shop.SetActive(true);
    }
    public void ShowTowerMenu()
    {
        TowerMenu.SetActive(true);
    }
    public void ShowExitConfirm()
    {
        ExitConfirm.SetActive(true);
    }
    #endregion

}
