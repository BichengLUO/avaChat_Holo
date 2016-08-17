using UnityEngine;
using System.Collections.Generic;

public class RecentListManager : MonoBehaviour {
    public GameObject listItemPrefab;
    public List<GameObject> recentList;

    public void SetRecent(List<Conversation> conversations)
    {
        for (int i = 0; i < conversations.Count; i++)
        {
            Conversation conv = conversations[i];
            GameObject listItem = Instantiate(listItemPrefab) as GameObject;
            recentList.Add(listItem);
            ListItem li = listItem.GetComponent<ListItem>();
            li.thumbID = conv.charId;
            li.userName = conv.name;
            li.topic = conv.topic;
            li.conv = conv;
            listItem.transform.position = new Vector3(0, -0.3f * i, 3);
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
