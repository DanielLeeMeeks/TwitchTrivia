using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    chatDecoder cd;
    TwitchIRC irc;
    questionManager qm;
    uiManager ui;

    public enum steps {question, lag, answer};
    public steps currentStep;

    public int questionTime = 30;
    public int lagTime = 5;
    public int answerTime = 15;
    public int tickTime;

    public bool votingOpen = false;
    public List<playerObj> players = new List<playerObj>();

	// Use this for initialization
	void Start () {
        cd = this.GetComponent<chatDecoder>();
        irc = this.GetComponent<TwitchIRC>();
        qm = this.GetComponent<questionManager>();
        ui = this.GetComponent<uiManager>();
        qm.loadNewQuestion();

        InvokeRepeating("tick", 5, 1);

	}

    void tick() {
        tickTime -= 1;
        if (tickTime < 0)
        {
            changeStep();
        }
        else {
            
        }

        if (currentStep == steps.question)
        {
            ui.addToFooter("Submit your answer now (" + tickTime + ")");
        }
        else if (currentStep == steps.lag)
        {
            ui.replaceFooter("Waiting for stream lag... (" + tickTime + ")");
        }
        else if (currentStep == steps.answer) {
            ui.addToFooter("Get ready for the next question... ("+tickTime+")");
        }

    }

    void changeStep() {
        if (currentStep == steps.question)
        {
            currentStep = steps.lag;
            tickTime = lagTime;
        }
        else if (currentStep == steps.lag)
        {
            votingOpen = false;

            foreach (playerObj p in players) {
                if (p.getAnswer() == qm.getQuestion().correct_answer_id)
                {
                    p.addScore(1);
                    p.addBouns();
                }
                else if (p.getAnswer() == -2)
                {
                    //TODO: go with crowd.
                    Debug.LogWarning("TODO: go with crowd.");
                }
                else
                {
                    p.setBonus(0);
                }
            }

            currentStep = steps.answer;
            tickTime = answerTime;
            ui.showCorrectAnswer(qm.getQuestion());
            qm.loadNewQuestion();
        }
        else if (currentStep == steps.answer) {
            currentStep = steps.question;
            votingOpen = true;
            tickTime = questionTime;
            
            ui.setQuestion(qm.getQuestion());
        }
        else
        {
            Debug.LogWarning("Step Error.  Stepping to new question.");
            currentStep = steps.answer;
        }
    }

    public playerObj findPlayer(string username) {
        foreach (playerObj p in players) {
            if (username == p.getUsername()) {
                return p;
            }
        }
        return addPlayer(username);
    }

    playerObj addPlayer(string username) {
        playerObj p = new playerObj(username);
        players.Add(p);
        return p;
    }

}
