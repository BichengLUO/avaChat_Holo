using UnityEngine;
using System.Collections;

public class CloseBtnClick : MonoBehaviour {

	void OnClick()
    {
        Destroy(transform.parent.gameObject);
    }
}
