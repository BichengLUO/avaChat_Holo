using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class User
{
    public string userID;
    public string userName;
    public int charID;

    public User(string uid, string uname, int cid)
    {
        userID = uid;
        userName = uname;
        charID = cid;
    }
}
