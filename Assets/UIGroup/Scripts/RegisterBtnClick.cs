using UnityEngine;
using System.Collections;

public class RegisterBtnClick : MonoBehaviour {
    public GameObject registerGroup;
    public GameObject registerGroupPrefab;

    void OnClick()
    {
        Destroy(transform.parent.gameObject);
        registerGroup = Instantiate(registerGroupPrefab);
    }
}
