using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatDecoder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void chat(string username, string message) {
        Debug.Log(username + " said, \"" + message + "\".");
    }
}
