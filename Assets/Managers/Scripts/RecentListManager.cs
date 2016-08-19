using UnityEngine;
using System.Collections.Generic;

public class RecentListManager : MonoBehaviour {
    public GameObject listItemPrefab;
    public List<GameObject> recentList;

    public void SetRecent(List<Conversation> conversations)
    {
        ClearRecent();
        for (int i = 0; i < conversations.Count; i++)
        {
            Conversation conv = conversations[i];
            GameObject listItem = Instantiate(listItemPrefab) as GameObject;
            recentList.Add(listItem);
            ListItem li = listItem.GetComponent<ListItem>();
            li.conv = conv;
            listItem.transform.position = new Vector3(1.3f, -0.3f * i + 0.3f, 3.28f);
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
