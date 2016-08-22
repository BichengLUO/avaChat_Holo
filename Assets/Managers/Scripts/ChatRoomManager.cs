using UnityEngine;
using System.Collections;

public class ChatRoomManager : MonoBehaviour {
    public GameObject chatRoomPrefab;
    public GameObject chatRoom;
    public GameObject messageBoxPrefab;

    void Start()
    {
        Words2Anim.LoadVocabulary();
    }

    public void SetChatRoom(User user)
    {
        StartCoroutine(ChatManager.createChat(user,
            (conv) => SetChatRoom(conv),
            (msg) => fail(msg)));
    }

    public void fail(string msg)
    {
        GameObject msgBox = Instantiate(messageBoxPrefab) as GameObject;
        MessageBox mb = msgBox.GetComponent<MessageBox>();
        mb.message = msg;
    }

    public void SetChatRoom(Conversation conv)
    {
        Destroy(chatRoom);
        chatRoom = Instantiate(chatRoomPrefab);
        ChatRoom cr = chatRoom.GetComponent<ChatRoom>();
        cr.conv = conv;
    }
}
