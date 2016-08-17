using UnityEngine;
using System.Collections;

public class PswdRegisterBtnClick : MonoBehaviour {
    public TextBox userNameTextBox;
    public PasswordBox passwordBox;
    public PasswordBox confirmPasswordBox;
    public LoginManager loginManager;

    void OnClick()
    {
        StartCoroutine(ChatManager.register(userNameTextBox.text, passwordBox.password, (token) => success(token)));
    }

    void success(string token)
    {
        Destroy(transform.parent.gameObject);
        loginManager = GameObject.Find("Managers").GetComponent<LoginManager>();
        loginManager.success(token);
    }
}
