using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Conversation
{
    public string convId;
    public string name;
    public string creatorName;
    public List<string> memberNames;
    public string topic;
    public int charId;

    public Conversation(string cid, int chid, string n, string c, List<string> m, string t = null)
    {
        convId = cid;
        charId = chid;
        name = n;
        creatorName = c;
        memberNames = m;
        topic = t;
    }
}
