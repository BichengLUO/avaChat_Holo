using UnityEngine;
using System.Collections.Generic;

public class SearchBtnClick : MonoBehaviour {
    public GameObject listItemPrefab;
    public List<GameObject> usersList;
    public TextBox searchTextBox;
    public GameObject loadingPrefab;
    public GameObject loading;

    public void StartLoading()
    {
        loading = Instantiate(loadingPrefab);
        loading.transform.position = transform.position;
        loading.transform.Translate(0.3f, 0, 0);
        loading.transform.parent = transform;
    }

    public void EndLoading()
    {
        Destroy(loading);
    }

    void OnClick()
    {
        string friendName = searchTextBox.text;
        StartLoading();
        StartCoroutine(ChatManager.searchUser(friendName, (users) => success(users)));
    }

    void success(List<User> users)
    {
        ClearSearch();
        EndLoading();
        for (int i = 0; i < users.Count; i++)
        {
            User user = users[i];
            GameObject searchItem = Instantiate(listItemPrefab) as GameObject;
            usersList.Add(searchItem);
            SearchItem si = searchItem.GetComponent<SearchItem>();
            si.user = user;
            searchItem.transform.parent = transform.parent;
            searchItem.transform.position = new Vector3(0.2f, -0.3f * i + 0.1f, 3.2f);
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
