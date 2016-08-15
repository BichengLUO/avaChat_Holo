using UnityEngine;
using System.Collections;

public class LoginBtnClick : MonoBehaviour {
    public GameObject loginGroup;
    public GameObject loginGroupPrefab;

	void OnClick()
    {
        loginGroup = Instantiate(loginGroupPrefab);
        Destroy(transform.parent.gameObject);
    }
}
