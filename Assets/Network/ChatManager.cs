using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;

public class ChatManager : MonoBehaviour {
    private static string leancloudID = "EILoQAne46y25Uv01de5sfO4-gzGzoHsz";
    private static string leancloudKey = "csA7lTvQYOWmlrsrfzimzzYK";

    private static string userID;
    private static string sessionToken;
    private static Dictionary<string, string> headers = new Dictionary<string, string>();

    public static void initHeaders()
    {
        headers.Add("X-LC-Id", leancloudID);
        headers.Add("X-LC-Key", leancloudKey);
        headers.Add("Content-Type", "application/json");
    }

    public static IEnumerator register(string userName, string password)
    {
        byte[] postData = Encoding.ASCII.GetBytes(string.Format("{{\"username\":\"{0}\",\"password\":\"{1}\"}}", userName, password));
        WWW www = new WWW("https://api.leancloud.cn/1.1/users", postData, headers);
        yield return www;
        var ret = JSON.Parse(www.text);
        userID = ret["objectId"].Value;
        sessionToken = ret["sessionToken"].Value;
    }

    public static IEnumerator login(string userName, string password)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string url = string.Format("https://api.leancloud.cn/1.1/login?username={0}&password={1}", WWW.EscapeURL(userName), WWW.EscapeURL(password));
        WWW www = new WWW(url);
        yield return www;
        var ret = JSON.Parse(www.text);
        userID = ret["objectId"].Value;
        sessionToken = ret["sessionToken"].Value;
    }

    public static IEnumerator follow(string otherID)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string url = string.Format("https://leancloud.cn/1.1/users/{0}/friendship/{1}", userID, otherID);
        WWW www = new WWW(url, new byte[] { }, headers);
        yield return www;
    }

    public static IEnumerator getFollowers()
    {
        string url = string.Format("https://leancloud.cn/1.1/users/{0}/followers?include=follower", userID);
        WWW www = new WWW(url, null, headers);
        yield return www;
        var ret = JSON.Parse(www.text);

    }


}
