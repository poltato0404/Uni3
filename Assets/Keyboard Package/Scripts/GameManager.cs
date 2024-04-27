using UnityEngine;
using TMPro;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;
    public TextMeshProUGUI textBox;
    public FakeTerminal term;
    public bool pressed;
    public bool backspace;
    public bool Return;
    

    private void Start()
    {
        pressed = false;
        backspace = false;
        Return = false;
        Instance = this;
        textBox.text = "";
    }

    public void DeleteLetter()
    {
        pressed = true;
        backspace = true;
        if(textBox.text.Length != 0) {
            //textBox.text = term.outputText.text;
        }
        

    }

    public void AddLetter(string letter)
    {
        textBox.text = textBox.text + letter;
        pressed = true;
    }

    public void SubmitWord()
    {
        
        pressed = true;
        Return = true;
        
        // Debug.Log("Text submitted successfully!");
    }
}
