using UnityEngine;
using System.Collections;

public class LoginManager : MonoBehaviour {
    public GameObject accountGroupPrefab;
    public GameObject accountGroup;
    public GameObject searchGroupPrefab;
    public GameObject searchGroup;

    public FriendsListManager friendsListManager;
    public RecentListManager recentListManager;

    // Use this for initialization
    void Start () {
        friendsListManager = GetComponent<FriendsListManager>();
        recentListManager = GetComponent<RecentListManager>();
        string sessionToken = PlayerPrefs.GetString("session");
        if (sessionToken == null || sessionToken == "")
            accountGroup = Instantiate(accountGroupPrefab) as GameObject;
        else
            StartCoroutine(ChatManager.login(sessionToken, (token) => success(token)));
	}
	
    public void success(string token)
    {
        StartCoroutine(ChatManager.getRecent((recent) => recentListManager.SetRecent(recent)));

        searchGroup = Instantiate(searchGroupPrefab) as GameObject;
    }
}
