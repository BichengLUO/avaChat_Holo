using UnityEngine;
using System.Collections.Generic;

public class SearchBtnClick : MonoBehaviour {
    public GameObject listItemPrefab;
    public List<GameObject> usersList;
    public TextBox searchTextBox;

	void OnClick()
    {
        string friendName = searchTextBox.text;
        StartCoroutine(ChatManager.searchUser(friendName, (users) => success(users)));
    }

    void success(List<User> users)
    {
        ClearSearch();
        for (int i = 0; i < users.Count; i++)
        {
            User user = users[i];
            GameObject listItem = Instantiate(listItemPrefab) as GameObject;
            usersList.Add(listItem);
            ListItem li = listItem.GetComponent<ListItem>();
            li.thumbID = user.charID;
            li.userName = user.userName;
            li.topic = "";
            li.user = user;
            listItem.transform.parent = transform.parent;
            listItem.transform.position = new Vector3(-0.5f, -0.3f * i + 0.3f, 3);
        }
    }

    void ClearSearch()
    {
        foreach (GameObject friendItem in usersList)
        {
            Destroy(friendItem);
        }
        usersList.Clear();
    }
}
