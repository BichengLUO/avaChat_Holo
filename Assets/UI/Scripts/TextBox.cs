using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {
    public TouchScreenKeyboard keyboard;
    public string text;
    public Text uiText;
    private Renderer rend;

    void OnSelect()
    {
        keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.Default, false, false, false, false);
    }

    public void OnGazeEnter()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
    }

    public void OnGazeLeave()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
    }

    void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.done == true)
            {
                text = keyboard.text;
                uiText.text = keyboard.text;
                keyboard = null;
            }
        }
    }
}
