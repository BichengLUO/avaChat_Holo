using UnityEngine;
using System.Collections.Generic;

public class FriendsListManager : MonoBehaviour {
    public GameObject listItemPrefab;
    public List<GameObject> friendsList;

    public void SetFriends(List<User> friends)
    {
        for (int i = 0; i < friends.Count; i++)
        {
            User friend = friends[i];
            GameObject listItem = Instantiate(listItemPrefab) as GameObject;
            friendsList.Add(listItem);
            ListItem li = listItem.GetComponent<ListItem>();
            li.thumbID = friend.charID;
            li.userName = friend.userName;
            li.topic = "";
            li.user = friend;
            listItem.transform.position = new Vector3(0, -0.3f * i, 3);
        }
    }

    public void ClearRecent()
    {
        foreach (GameObject friendItem in friendsList)
        {
            Destroy(friendItem);
        }
        friendsList.Clear();
    }
}
