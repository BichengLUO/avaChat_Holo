using UnityEngine;
using System.Collections;

public class LoginManager : MonoBehaviour {
    public GameObject accountGroupPrefab;
    public GameObject searchGroupPrefab;
    public GameObject messageBoxPrefab;

    public GameObject friendsGroupPrefab;
    public GameObject recentGroupPrefab;
    public GameObject profileGroupPrefab;

    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
        string sessionToken = PlayerPrefs.GetString("session");
        if (sessionToken == null || sessionToken == "")
            Instantiate(accountGroupPrefab);
        else
            StartCoroutine(ChatManager.login(sessionToken,
                (token) => success(token),
                (msg) => fail(msg)));
	}
	
    public void success(string token)
    {
        GameObject friendsGroup = Instantiate(friendsGroupPrefab);
        GameObject recentGroup = Instantiate(recentGroupPrefab);
        GameObject searchGroup = Instantiate(searchGroupPrefab);
        GameObject profileGroup = Instantiate(profileGroupPrefab);
        friendsGroup.name = "FriendsGroup";
        recentGroup.name = "RecentGroup";
        searchGroup.name = "SearchGroup";
        profileGroup.name = "ProfileGroup";
    }

    public void fail(string msg)
    {
        GameObject msgBox = Instantiate(messageBoxPrefab) as GameObject;
        MessageBox mb = msgBox.GetComponent<MessageBox>();
        mb.message = msg;
    }
}
