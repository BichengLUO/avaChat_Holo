using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using SimpleJSON;

public class ChatManager : MonoBehaviour {
    private static string leancloudID = "EILoQAne46y25Uv01de5sfO4-gzGzoHsz";
    private static string leancloudKey = "csA7lTvQYOWmlrsrfzimzzYK";

    public static User currentUser;
    private static Dictionary<string, string> headers = new Dictionary<string, string>();
    private static int MaxCount = 9;

    public void Start()
    {
        initHeaders();
    }

    public static void initHeaders()
    {
        headers.Add("X-LC-Id", leancloudID);
        headers.Add("X-LC-Key", leancloudKey);
        headers.Add("Content-Type", "application/json");
    }

    public static int getHashCharID(string userName)
    {
        int hash = 0;
        for (int i = 0; i < userName.Length; i++)
        {
            char ch = userName[i];
            hash += ch;
        }
        return hash % MaxCount;
    }

    public static IEnumerator register(string userName, string password, Action<string> callback = null, Action<string> error = null)
    {
        int charID = getHashCharID(userName);
        return register(userName, password, charID, callback, error);
    }

    public static IEnumerator register(string userName, string password, int charID, Action<string> callback = null, Action<string> error = null)
    {
        byte[] postData = Encoding.ASCII.GetBytes(string.Format("{{\"username\":\"{0}\",\"password\":\"{1}\",\"charid\":{2}}}", userName, password, charID));
        WWW www = new WWW("https://api.leancloud.cn/1.1/users", postData, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            string sessionToken = ret["sessionToken"].Value;
            string userID = ret["objectId"].Value;
            currentUser = new User(userID, userName, charID);
            headers.Add("X-LC-Session", sessionToken);
            PlayerPrefs.SetString("session", sessionToken);
            if (callback != null)
                callback(sessionToken);
        }
    }

    public static IEnumerator login(string userName, string password, Action<string> callback = null, Action<string> error = null)
    {
        string url = string.Format("https://api.leancloud.cn/1.1/login?username={0}&password={1}", WWW.EscapeURL(userName), WWW.EscapeURL(password));
        WWW www = new WWW(url, null, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            string sessionToken = ret["sessionToken"].Value;
            string userID = ret["objectId"].Value;
            int charID = ret["charid"].AsInt;
            currentUser = new User(userID, userName, charID);
            headers.Add("X-LC-Session", sessionToken);
            PlayerPrefs.SetString("session", sessionToken);
            if (callback != null)
                callback(sessionToken);
        }
    }

    public static IEnumerator login(string token, Action<string> callback = null, Action<string> error = null)
    {
        headers.Add("X-LC-Session", token);
        WWW www = new WWW("https://api.leancloud.cn/1.1/users/me", null, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            string userID = ret["objectId"].Value;
            string userName = ret["username"].Value;
            int charID = ret["charid"].AsInt;
            currentUser = new User(userID, userName, charID);
            if (callback != null)
                callback(headers["X-LC-Session"]);
        }
    }

    public static IEnumerator follow(User other, Action callback = null, Action<string> error = null)
    {
        string url = string.Format("https://leancloud.cn/1.1/users/self/friendship/{0}", other.userID);
        byte[] postData = Encoding.ASCII.GetBytes(string.Format("{{\"fake\":0}}"));
        WWW www = new WWW(url, postData, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            if (callback != null)
                callback();
        }
    }

    public static IEnumerator searchUser(string userName, Action<List<User>> callback = null, Action<string> error = null)
    {
        string cql = string.Format("select * from _User where username like '{0}'", userName);
        string url = string.Format("https://api.leancloud.cn/1.1/cloudQuery?cql={0}", WWW.EscapeURL(cql));
        WWW www = new WWW(url, null, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            List<User> users = new List<User>();
            for (int i = 0; i < ret["results"].AsArray.Count; i++)
            {
                string userId = ret["results"][i]["objectId"].Value;
                string uName = ret["results"][i]["username"].Value;
                int charId = ret["results"][i]["charid"].AsInt;

                users.Add(new User(userId, uName, charId));
            }
            if (callback != null)
                callback(users);
        }
    }

    public static IEnumerator getFollowers(Action<List<User>> callback = null, Action<string> error = null)
    {
        string url = string.Format("https://leancloud.cn/1.1/users/{0}/followers?include=follower", currentUser.userID);
        WWW www = new WWW(url, null, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            List<User> followers = new List<User>();
            for (int i = 0; i < ret["results"].AsArray.Count; i++)
            {
                var follower = ret["results"][i]["follower"];
                string userId = follower["objectId"].Value;
                string userName = follower["username"].Value;
                int charId = follower["charid"].AsInt;

                followers.Add(new User(userId, userName, charId));
            }
            if (callback != null)
                callback(followers);
        }
    }

    public static IEnumerator createChat(User other, Action<string> callback = null, Action<string> error = null)
    {
        byte[] postData = Encoding.ASCII.GetBytes(string.Format("{{\"name\":\"{0}\",\"c\":\"{1}\",\"m\":[\"{1}\",\"{0}\"],\"charid\":{2}}}", other.userName, currentUser.userName, other.charID));
        WWW www = new WWW("https://api.leancloud.cn/1.1/classes/_Conversation", postData, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            string convId = ret["objectId"].Value;
            if (callback != null)
                callback(convId);
        }
    }

    public static IEnumerator sendMessage(string convId, string otherName, string message, Action callback = null, Action<string> error = null)
    {
        byte[] postData = Encoding.ASCII.GetBytes(string.Format("{{\"from_peer\":\"{0}\",\"to_peers\":[\"{1}\"],\"message\":\"{{\\\"_lctype\\\":-1,\\\"_lctext\\\":\\\"{2}\\\"}}\",\"conv_id\":\"{3}\",\"transient\": false}}", currentUser.userName, otherName, message, convId));
        WWW www = new WWW("https://leancloud.cn/1.1/rtm/messages", postData, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            postData = Encoding.ASCII.GetBytes(string.Format("{{\"topic\":\"{0}\"}}", message));
            string url = string.Format("https://api.leancloud.cn/1.1/classes/_Conversation/{0}", convId);
            UnityWebRequest request = UnityWebRequest.Put(url, postData);
            foreach (KeyValuePair<string, string> entry in headers)
                request.SetRequestHeader(entry.Key, entry.Value);
            yield return request.Send();
            if (callback != null)
                callback();
        }
    }

    public static IEnumerator getChatHistory(string convId, Action<List<Message>> callback = null, Action<string> error = null)
    {
        string url = string.Format("https://leancloud.cn/1.1/rtm/messages/logs?convid={0}", convId);
        WWW www = new WWW(url, null, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            List<Message> messages = new List<Message>();
            for (int i = 0; i < ret.AsArray.Count; i++)
            {
                string from = ret[i]["from"].Value;
                string data = ret[i]["data"].Value;
                int timestamp = ret[i]["timestamp"].AsInt;
                messages.Add(new Message(convId, from, data, timestamp));
            }
            if (callback != null)
                callback(messages);
        }
    }

    public static IEnumerator getRecent(Action<List<Conversation>> callback = null, Action<string> error = null)
    {
        string url = string.Format("https://api.leancloud.cn/1.1/classes/_Conversation?where={{\"m\":\"{0}\"}}", currentUser.userName);
        WWW www = new WWW(url, null, headers);
        yield return www;
        Debug.Log(www.text);
        var ret = JSON.Parse(www.text);
        string errorMsg = ret["error"].Value;
        if (errorMsg != "")
        {
            if (error != null)
                error(errorMsg);
        }
        else
        {
            List<Conversation> conversations = new List<Conversation>();
            for (int i = 0; i < ret["results"].AsArray.Count; i++)
            {
                string convId = ret["results"][i]["objectId"].Value;
                string name = ret["results"][i]["name"].Value;
                string topic = ret["results"][i]["topic"].Value;
                int charId = ret["results"][i]["charid"].AsInt;
                string creatorName = ret["results"][i]["c"].Value;
                List<string> memberNames = new List<string>();
                for (int j = 0; j < ret["results"][i]["m"].AsArray.Count; j++)
                {
                    memberNames.Add(ret["results"][i]["m"][j].Value);
                }
                conversations.Add(new Conversation(convId, charId, name, creatorName, memberNames, topic));
            }
            if (callback != null)
                callback(conversations);
        }
    }
}
