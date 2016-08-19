using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Message
{
    public string msgId;
    public string convId;
    public string from;
    public string data;
    public int timestamp;

    public Message(string mid, string cid, string fr, string dt, int tst)
    {
        msgId = mid;
        convId = cid;
        from = fr;
        data = dt;
        timestamp = tst;
    }
}
