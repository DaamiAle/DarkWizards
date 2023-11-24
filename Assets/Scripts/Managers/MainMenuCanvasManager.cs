using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasManager : MonoBehaviour
{
    [SerializeField] Image TransitionImage, Background;
    [SerializeField] GameObject[] Menues; //Main, LevelMenu, Options, Credits;
    [SerializeField] int currentMenu,levelToGo;
    [SerializeField] TextMeshProUGUI textComponent;
    private bool isOnTransition,goOut,goIn;
    private void Awake()
    {
        isOnTransition = false;
        goOut = false;
        goIn = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentMenu = GameManager.Instance.ComeFromLevel ? 1 : 0;
        Background.gameObject.SetActive(currentMenu == 0);
        UpdateMenues();
        GameManager.Instance.ComeFromLevel = false;
        Invoke("Clarify", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (GameManager.Instance.LevelSupered > -1)
        {
            for (int i = 0; i < GameManager.Instance.LevelSupered + 2; i++)
            {
                Button button = Levels[i].GetComponent<Button>();
                if (!button.interactable) button.interactable = true;
                if (i< GameManager.Instance.LevelSupered+1) Levels[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        */
        /*
        if (goToMain)
        {
            levelManager.OutTransition();
            goToMain = false;
        }
        if (goToOptions)
        {
            levelManager.OutTransition();
            goToOptions = false;
        }
        if (goToCredits)
        {
            levelManager.OutTransition();
            goToCredits = false;
        }
        if (goToLevels)
        {
            levelManager.OutTransition();
            goToLevels = false;
        }
        if (goBackToLevels)
        {
            levelManager.OutTransition();
            goBackToLevels = false;
        }
        */
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
        if(currentMenu == 3)
        {
            textComponent.rectTransform.position += new Vector3(0,Time.deltaTime,0);
        }
        else
        {
            if(isOnTransition && goOut)
            {
                textComponent.rectTransform.position += new Vector3(0, Time.deltaTime, 0);
            }
            Invoke("ResetCredits", 2f);
        }
    }
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
    
    private void UpdateMenues()
    {
        for (int i = 0; i < Menues.Length; i++)
        {
            Menues[i].SetActive(i == currentMenu);
        }
        Background.gameObject.SetActive(currentMenu == 0);
    }

    public void OpenMainMenu()
    {
        Darken();
        currentMenu = 0;
        Invoke("UpdateMenues", 2f);
        Invoke("Clarify", 3f);
    }
    public void OpenLevelMenu()
    {
        Darken();
        currentMenu = 1;
        Invoke("UpdateMenues", 2f);
        Invoke("Clarify", 3f);
    }
    public void OpenOptions()
    {
        Darken();
        currentMenu = 2;
        Invoke("UpdateMenues",2f);
        Invoke("Clarify", 3f);
    }
    public void OpenCredits()
    {
        Darken();
        currentMenu = 3;
        Invoke("UpdateMenues", 2f);
        Invoke("Clarify", 3f);
        //ResetCredits();
    }
    public void SelectLevel(int level)
    {
        Darken();
        currentMenu = 1;
        levelToGo = level;
        Invoke("GoToLevel",2F);
    }
    private void GoToLevel()
    {
        GameManager.Instance.StartLevel(levelToGo);
    }
    private void ResetCredits()
    {
        textComponent.rectTransform.localPosition = new Vector3(0, -140, 0);
    }
    public void ExitGame()
    {
        Darken();
        Invoke("QuitGame", 2f);
    }
    private void QuitGame()
    {
        GameManager.Instance.Quit();
    }
}
