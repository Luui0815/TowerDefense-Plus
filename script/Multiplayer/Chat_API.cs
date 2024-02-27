
using Godot;
using System;
using System.Collections.Generic;

public class Chat_API
{
    public class msg_properties
    {
        public DateTime SendTime;
        public string AuthorName;
        public string msg;
        public msg_properties(string author, string msg)
        {
            SendTime=DateTime.Now;
            AuthorName=author;
            this.msg = msg;
        }
    }
    private static string _newestMsg;
    private static bool _NewMsgAvailable=false;
    private static List<msg_properties> _Chat = new List<msg_properties>();

    public static void SendNewMsg(string author, string msg)
    {
        _NewMsgAvailable= true;
        _Chat.Add(new msg_properties(author, msg));
        _newestMsg = msg;
    }

    public static string NewMsg
    {
        get
        {
            _NewMsgAvailable=false;
            return _newestMsg;
        }
    }

    public static List<msg_properties> Chat_Archiev
    {
        get
        {
            return _Chat;
        }
    }

    public static bool NewMsgAvailable
    {
        get
        {
            return _NewMsgAvailable;
        }
    }
}
