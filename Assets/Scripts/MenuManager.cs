using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    DialogueManager dialogueManager;
    public GameObject menuButtons;
    public GameObject clickAnywhere;
    /*public GameObject backButton;
    public GameObject controlInfo;*/


    // Start is called before the first frame update
    void Start()
    {
        menuButtons.SetActive(true);
        clickAnywhere.SetActive(true);
        /*backButton.SetActive(false);
        controlInfo.SetActive(false); */       
    }

    public void DisplayControls()
    {
        /*menuButtons.SetActive(false);
        controlInfo.SetActive(true);
        backButton.SetActive(true);*/
    }

    public void Back()
    {
        /*menuButtons.SetActive(true);
        controlInfo.SetActive(false);
        backButton.SetActive(false);*/
    }

    public void StartGameSplash()
    {
        //there will need to be some sort of condition to activate the backstory
        //but for now it'll activate it every time
        SceneManager.LoadScene("NewGameSplash");
    }

    public void ReadGameSplash()
    {
        dialogueManager = GameObject.Find("Canvas").GetComponent<DialogueManager>();
        
        dialogueManager.textComponent.text = string.Empty;
        dialogueManager.StartDialogue();
        clickAnywhere.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

}
