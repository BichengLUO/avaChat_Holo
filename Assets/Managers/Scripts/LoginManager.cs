using UnityEngine;
using System.Collections;

public class LoginManager : MonoBehaviour {
    public GameObject accountGroupPrefab;
    public GameObject searchGroupPrefab;
    public GameObject messageBoxPrefab;
    public GameObject listItemPrefab;

    public GameObject friendsGroupPrefab;
    public GameObject recentGroupPrefab;

    // Use this for initialization
    void Start () {
        string sessionToken = PlayerPrefs.GetString("session");
        if (sessionToken == null || sessionToken == "")
            Instantiate(accountGroupPrefab);
        else
            StartCoroutine(ChatManager.login(sessionToken,
                (token) => success(token),
                (msg) => fail(msg)));
	}

    public void fail(string msg)
    {
        GameObject msgBox = Instantiate(messageBoxPrefab) as GameObject;
        MessageBox mb = msgBox.GetComponent<MessageBox>();
        mb.message = msg;
    }
	
    public void success(string token)
    {
        GameObject friendsGroup = Instantiate(friendsGroupPrefab);
        GameObject recentGroup = Instantiate(recentGroupPrefab);
        GameObject searchGroup = Instantiate(searchGroupPrefab);
        GameObject selfItem = Instantiate(listItemPrefab);
        friendsGroup.name = "FriendsGroup";
        recentGroup.name = "RecentGroup";
        searchGroup.name = "SearchGroup";
        selfItem.name = "SelfItem";

        FriendsListManager friendsListManager = friendsGroup.GetComponent<FriendsListManager>();
        RecentListManager recentListManager = recentGroup.GetComponent<RecentListManager>();
        StartCoroutine(ChatManager.getRecent(
            (recent) => recentListManager.SetRecent(recent),
            (msg) => fail(msg)));
        StartCoroutine(ChatManager.getFollowers(
            (friends) => friendsListManager.SetFriends(friends),
            (msg) => fail(msg)));
        
        selfItem.transform.position = new Vector3(-0.62f, 0.86f, 3.15f);
        ListItem selfListItem = selfItem.GetComponent<ListItem>();
        selfListItem.user = ChatManager.currentUser;
    }
}
