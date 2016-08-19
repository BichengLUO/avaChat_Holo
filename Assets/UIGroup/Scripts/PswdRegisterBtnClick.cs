using UnityEngine;
using System.Collections;

public class PswdRegisterBtnClick : MonoBehaviour {
    public TextBox userNameTextBox;
    public PasswordBox passwordBox;
    public PasswordBox confirmPasswordBox;
    public LoginManager loginManager;
    public GameObject loadingPrefab;
    public GameObject loading;

    public void StartLoading()
    {
        loading = Instantiate(loadingPrefab);
        loading.transform.position = transform.position;
        loading.transform.Translate(0.3f, 0, 0);
        loading.transform.parent = transform;
    }

    public void EndLoading()
    {
        Destroy(loading);
    }

    void OnClick()
    {
        if (passwordBox.password != confirmPasswordBox.password)
            fail("Passwords don't match.");
        else
        {
            StartLoading();
            StartCoroutine(ChatManager.register(userNameTextBox.text, passwordBox.password,
            (token) => success(token),
            (msg) => fail(msg)));
        }
    }

    void fail(string msg)
    {
        EndLoading();
        loginManager = GameObject.Find("Managers").GetComponent<LoginManager>();
        loginManager.fail(msg);
    }

    void success(string token)
    {
        EndLoading();
        Destroy(transform.parent.gameObject);
        loginManager = GameObject.Find("Managers").GetComponent<LoginManager>();
        loginManager.success(token);
    }
}
