using UnityEngine;
using System.Collections;

public class PswdRegisterBtnClick : MonoBehaviour {
    public TextBox userNameTextBox;
    public PasswordBox passwordBox;
    public PasswordBox confirmPasswordBox;
    public LoginManager loginManager;

    void OnClick()
    {
        if (passwordBox.password != confirmPasswordBox.password)
            fail("Passwords don't match.");
        else
        {
            StartCoroutine(ChatManager.register(userNameTextBox.text, passwordBox.password,
            (token) => success(token),
            (msg) => fail(msg)));
        }
    }

    void fail(string msg)
    {
        loginManager = GameObject.Find("Managers").GetComponent<LoginManager>();
        loginManager.fail(msg);
    }

    void success(string token)
    {
        Destroy(transform.parent.gameObject);
        loginManager = GameObject.Find("Managers").GetComponent<LoginManager>();
        loginManager.success(token);
    }
}
