using UnityEngine;
using System.Collections;

public class ChatRoomCloseBtnClick : MonoBehaviour {

    void OnClick()
    {
        Destroy(transform.parent.gameObject);
    }
}
