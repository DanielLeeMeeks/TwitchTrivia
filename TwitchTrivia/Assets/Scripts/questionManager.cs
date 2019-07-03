using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionManager : MonoBehaviour {

    questionObj q = new questionObj();
    //uiManager ui;
    string api = "https://opentdb.com/api.php";

	// Use this for initialization
	void Start () {
        //ui = this.GetComponent<uiManager>();

        //loadNewQuestion();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public questionObj getQuestion() { return q; }

    public void loadNewQuestion() {
        loadNewQuestion(1, "", "", "");
    }

    public void loadNewQuestion(int amount, string category, string difficulty, string type)
    {
        StartCoroutine(loadNewQuestion_ie(amount, category, difficulty, type));
    }

    public IEnumerator loadNewQuestion_ie(int amount, string category, string difficulty, string type) {
        string url = api + "?amount=" + amount + "&category=" + category + "&difficulty=" + difficulty + "&type=" + type;
        Debug.Log(url);
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError("ERROR DOWNLOADING QUESTION: " + www.error);
        }
        else
        {
            string wwwString = www.text;
            wwwString = wwwString.Replace("{\"response_code\":0,\"results\":[", "");
            wwwString = wwwString.Remove(wwwString.Length - 2);
            Debug.Log(wwwString);
            //q = (questionObj)JsonUtility.FromJson(wwwString, typeof(questionObj));
            q = JsonUtility.FromJson<questionObj>(wwwString);
            q.setAnswers();
            //Debug.Log(q.incorrect_answers[0]);
        }

        //ui.setQuestion(q);

    }

    public string fiftyFifty() { return q.fiftyFifty(); }
    }
