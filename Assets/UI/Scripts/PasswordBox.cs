using UnityEngine;
using UnityEngine.UI;

public class PasswordBox : MonoBehaviour {
    public TouchScreenKeyboard keyboard;
    public string password;
    public Text uiText;

    void OnSelect()
    {
        keyboard = TouchScreenKeyboard.Open(password, TouchScreenKeyboardType.Default, false, false, true, false);
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
