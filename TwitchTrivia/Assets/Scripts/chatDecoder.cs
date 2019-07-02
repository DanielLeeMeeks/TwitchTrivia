using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatDecoder : MonoBehaviour {

    gameManager gm;
    TwitchIRC irc;

	// Use this for initialization
	void Start () {
        gm = GetComponent<gameManager>();
        irc = GetComponent<TwitchIRC>();
	}

    public void chat(string username, string message) {
        message = message.ToUpper();
        Debug.Log(username + " said, \"" + message + "\".");
        if (gm.votingOpen)
        {
            if (message.Contains("#A"))
            {
                gm.findPlayer(username).setAnswer(0);
            }
            else if (message.Contains("#B") || message.Contains("#T"))
            {
                gm.findPlayer(username).setAnswer(1);
            }
            else if (message.Contains("#C"))
            {
                gm.findPlayer(username).setAnswer(2);
            }
            else if (message.Contains("#D") || message.Contains("#F"))
            {
                gm.findPlayer(username).setAnswer(3);
            }
            Debug.Log(username + " voted " + gm.findPlayer(username).getAnswer());
        }
        else
        {
            //Debug.Log("Voting has ended, @"+username + ".");
            //irc.SendMessage("@" + username + ", Voting has ended.");
            if (message.Contains("#A") || message.Contains("#B") || message.Contains("#C") || message.Contains("#D") || message.Contains("#T") || message.Contains("#F"))
            {
                SendMsg("@" + username + ", Voting has ended.");
            }
        }
        if (message.Contains("#SCORE")) {
            SendMsg("@" + username + ", your score is " + gm.findPlayer(username).getScore() + ".");
        }
    }

    void SendMsg(string msg) {
        lock (irc.commandQueue)
        {
            irc.commandQueue.Enqueue("PRIVMSG #" + irc.channelName + " :" + msg);
        }
    }
}
