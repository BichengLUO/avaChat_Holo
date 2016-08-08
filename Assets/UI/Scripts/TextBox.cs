using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {
    public TouchScreenKeyboard keyboard;
    public string text;
    public Text uiText;

    void OnSelect()
    {
        keyboard = TouchScreenKeyboard.Open(uiText.text, TouchScreenKeyboardType.Default, false, false, false, false);
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
