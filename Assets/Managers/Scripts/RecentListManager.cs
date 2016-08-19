using UnityEngine;
using System.Collections.Generic;

public class RecentListManager : MonoBehaviour {
    public GameObject listItemPrefab;
    public GameObject loadingPrefab;
    public GameObject loading;
    public List<GameObject> recentList;

    public void StartLoading()
    {
        loading = Instantiate(loadingPrefab);
        loading.transform.position = new Vector3(-1.3f, 0.3f, 3.28f);
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
        for (int i = 0; i < conversations.Count; i++)
        {
            Conversation conv = conversations[i];
            GameObject listItem = Instantiate(listItemPrefab) as GameObject;
            recentList.Add(listItem);
            ListItem li = listItem.GetComponent<ListItem>();
            li.conv = conv;
            listItem.transform.position = new Vector3(-1.3f, -0.3f * i + 0.3f, 3.28f);
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
    }
}
