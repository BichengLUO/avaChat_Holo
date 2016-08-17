using UnityEngine;
using System.Collections;

public class PswdLoginBtnClick : MonoBehaviour {
    public TextBox userNameTextBox;
    public PasswordBox passwordBox;
    public LoginManager loginManager;

    void OnClick()
    {
        StartCoroutine(ChatManager.login(userNameTextBox.text, passwordBox.password, (token) => success(token)));
    }

    void success(string token)
    {
        Destroy(transform.parent.gameObject);
        loginManager = GameObject.Find("Managers").GetComponent<LoginManager>();
        loginManager.success(token);
    }
}
