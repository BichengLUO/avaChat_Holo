using UnityEngine;
using System.Collections;

public class RegisterGroupBackBtnClick : MonoBehaviour {
    public GameObject acountGroup;
    public GameObject acountGroupPrefab;

    void OnClick()
    {
        Destroy(transform.parent.gameObject);
        acountGroup = Instantiate(acountGroupPrefab);
    }
}
