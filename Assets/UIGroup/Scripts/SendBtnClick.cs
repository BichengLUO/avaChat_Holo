using UnityEngine;
using System.Collections;

public class SendBtnClick : MonoBehaviour {
    public TextBox textBox;
    public Conversation conv;

	void OnClick()
    {
        if (conv != null)
        {
            StartCoroutine(ChatManager.sendMessage(conv, textBox.text));
        }
    }
}
