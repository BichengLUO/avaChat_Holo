using UnityEngine;
using System.Collections;

public class RegisterBtnClick : MonoBehaviour {
    public GameObject registerGroup;
    public GameObject registerGroupPrefab;

    void OnClick()
    {
        registerGroup = Instantiate(registerGroupPrefab);
        Destroy(transform.parent.gameObject);
    }
}
