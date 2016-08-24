using UnityEngine;
using System.Collections;

public class AvatarChangerCloseBtnClick : MonoBehaviour {

    void OnClick()
    {
        Destroy(transform.parent.gameObject);
    }
}
