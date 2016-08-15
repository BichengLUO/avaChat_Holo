using UnityEngine;
using System.Collections;

public class RegisterGroupBackBtnClick : MonoBehaviour {
    public GameObject acountGroup;
    public GameObject acountGroupPrefab;

    void OnClick()
    {
        acountGroup = Instantiate(acountGroupPrefab);
        Destroy(transform.parent.gameObject);
    }
}
