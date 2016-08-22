using UnityEngine;
using System.Collections.Generic;

public class FriendsListManager : MonoBehaviour {
    public GameObject listItemPrefab;
    public GameObject loadingPrefab;
    public GameObject loading;
    public List<GameObject> friendsList = new List<GameObject>();
    public ChatRoomManager chatRoomManager;

    public void StartLoading()
    {
        loading = Instantiate(loadingPrefab);
        loading.transform.position = new Vector3(1.3f, 0.3f, 3.28f);
        loading.transform.Rotate(Vector3.forward, -30);
        loading.transform.parent = transform;
    }

    public void EndLoading()
    {
        Destroy(loading);
    }

    public void SetFriends(List<User> friends)
    {
        ClearFriends();
        EndLoading();
        for (int i = 0; i < friends.Count; i++)
        {
            User friend = friends[i];
            GameObject listItem = Instantiate(listItemPrefab) as GameObject;
            friendsList.Add(listItem);
            ListItem li = listItem.GetComponent<ListItem>();
            li.user = friend;
            li.callback = (u, c) => ItemClick(u, c);
            listItem.transform.position = new Vector3(1.3f, -0.3f * i + 0.3f, 3.28f);
            listItem.transform.Rotate(Vector3.forward, -30);
            listItem.transform.parent = transform;
        }
    }

    public void ClearFriends()
    {
        foreach (GameObject friendItem in friendsList)
        {
            Destroy(friendItem);
        }
        friendsList.Clear();
    }

    public void ItemClick(User user, Conversation conv)
    {
        chatRoomManager = GameObject.Find("Managers").GetComponent<ChatRoomManager>();

        RecentListManager recentListManager = GameObject.Find("RecentGroup").GetComponent<RecentListManager>();
        foreach (Conversation c in recentListManager.convs)
        {
            if (c.memberNames.Count == 2 && c.memberNames.Contains(user.userName))
            {
                chatRoomManager.SetChatRoom(c);
                return;
            }
        }
        chatRoomManager.SetChatRoom(user);
    }
}
