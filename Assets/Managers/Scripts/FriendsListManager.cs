﻿using UnityEngine;
using System.Collections.Generic;

public class FriendsListManager : MonoBehaviour {
    public GameObject listItemPrefab;
    public GameObject loadingPrefab;
    public GameObject loading;
    public List<GameObject> friendsList;

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
}
