using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    V2PlayerControls v2PlayerControls;
    public GameObject dialogueBox;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public int index;
    public bool dialogueOver;
    


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        dialogueBox.SetActive(false);
        dialogueOver = true;
        //StartDialogue();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {                
                NextLine();
            }
            else
            {
                dialogueOver = true;
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        } 
        //Debug.Log(dialogueOver.ToString());       
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
        dialogueOver = false;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogue();
        }
    }
    public void CloseDialogue()
    {
        Debug.Log("Dialogue Over");
        dialogueOver = true;
        dialogueBox.SetActive(false);
    }
}
