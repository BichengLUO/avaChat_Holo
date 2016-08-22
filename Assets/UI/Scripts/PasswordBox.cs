using UnityEngine;
using UnityEngine.UI;

public class PasswordBox : MonoBehaviour {
    public TouchScreenKeyboard keyboard;
    public string password;
    public Text uiText;
    private Renderer rend;

    void OnSelect()
    {
        keyboard = TouchScreenKeyboard.Open(password, TouchScreenKeyboardType.Default, false, false, true, false);
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
                password = keyboard.text;
                uiText.text = new string('*', password.Length);
                keyboard = null;
            }
        }
    }
}
