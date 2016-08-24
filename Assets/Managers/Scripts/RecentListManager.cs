using UnityEngine;
using System.Collections.Generic;

public class RecentListManager : MonoBehaviour {
    public GameObject listItemPrefab;
    public GameObject loadingPrefab;
    public GameObject loading;
    public List<GameObject> recentList = new List<GameObject>();
    public List<Conversation> convs = new List<Conversation>();
    public ChatRoomManager chatRoomManager;
    public GameObject messageBoxPrefab;

    public void Start()
    {
        StartLoading();
        StartCoroutine(ChatManager.getRecent(
            (recent) => SetRecent(recent),
            (msg) => fail(msg)));
    }

    public void StartLoading()
    {
        loading = Instantiate(loadingPrefab);
        loading.transform.position = new Vector3(-1.82f, 0.3f, 3.08f);
        loading.transform.Rotate(Vector3.forward, 30);
        loading.transform.parent = transform;
    }

    public void EndLoading()
    {
        Destroy(loading);
    }

    public void SetRecent(List<Conversation> conversations)
    {
        ClearRecent();
        EndLoading();
        convs = conversations;
        for (int i = 0; i < conversations.Count; i++)
        {
            Conversation conv = conversations[i];
            GameObject listItem = Instantiate(listItemPrefab) as GameObject;
            recentList.Add(listItem);
            ListItem li = listItem.GetComponent<ListItem>();
            li.conv = conv;
            li.callback = (u, c) => ItemClick(u, c);
            listItem.transform.position = new Vector3(-1.82f, -0.3f * i + 0.3f, 3.08f);
            listItem.transform.Rotate(Vector3.forward, 30);
            listItem.transform.parent = transform;
        }
    }

    public void ClearRecent()
    {
        foreach (GameObject recentItem in recentList)
        {
            Destroy(recentItem);
        }
        recentList.Clear();
        convs.Clear();
    }

    public void ItemClick(User user, Conversation conv)
    {
        chatRoomManager = GameObject.Find("Managers").GetComponent<ChatRoomManager>();
        chatRoomManager.SetChatRoom(conv);
    }

    public void fail(string msg)
    {
        GameObject msgBox = Instantiate(messageBoxPrefab) as GameObject;
        MessageBox mb = msgBox.GetComponent<MessageBox>();
        mb.message = msg;
    }
}
