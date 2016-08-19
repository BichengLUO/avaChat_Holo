using UnityEngine;
using System.Collections;

public class LoginBtnClick : MonoBehaviour {
    public GameObject loginGroup;
    public GameObject loginGroupPrefab;

	void OnClick()
    {
        Destroy(transform.parent.gameObject);
        loginGroup = Instantiate(loginGroupPrefab);
    }
}
