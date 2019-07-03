using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour {

    chatDecoder cd;
    TwitchIRC irc;
    questionManager qm;
    uiManager ui;

    public Canvas scoreCanvas;
    public Canvas gameCanvas;

    public TextMeshProUGUI scoresLeft, scoresRight;

    public enum steps {question, lag, answer, scores};
    public steps currentStep;

    public int questionTime = 30;
    public int lagTime = 5;
    public int answerTime = 15;
    public int tickTime;
    public int questionsPerRound = 5;
    public int scoresTime = 30;
    int roundNumber;

    public bool votingOpen = false;
    public List<playerObj> players = new List<playerObj>();

	// Use this for initialization
	void Start () {
        cd = this.GetComponent<chatDecoder>();
        irc = this.GetComponent<TwitchIRC>();
        qm = this.GetComponent<questionManager>();
        ui = this.GetComponent<uiManager>();
        qm.loadNewQuestion();

        roundNumber = questionsPerRound;

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

            foreach (playerObj p in players)
            {
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
        else if (currentStep == steps.answer)
        {
            roundNumber -= 1;

            if (roundNumber < 0)
            {
                scoreCanvas.enabled = true;
                gameCanvas.enabled = false;
                currentStep = steps.scores;
                tickTime = scoresTime;
                players.Sort(playerObj.SortByScore);
                players.Reverse();
                Debug.Log(players.Count);
                int i = 0;
                scoresLeft.text = "";
                while (i < players.Count && i < 20) {
                    scoresLeft.text += players[i].getUsername() + " - " + players[i].getScore() + "\n";
                    i++;
                }
                i = 20;
                scoresRight.text = "";
                while (i < players.Count && i < 40)
                {
                    scoresLeft.text += players[i].getUsername() + " - " + players[i].getScore() + "\n";
                    i++;
                }

                roundNumber = questionsPerRound;
            }
            else
            {
                currentStep = steps.question;
                votingOpen = true;
                tickTime = questionTime;
                foreach (playerObj p in players) {
                    p.setAnswer(-1);
                }

                ui.setQuestion(qm.getQuestion());
            }
        }
        else if (currentStep == steps.answer) {
            currentStep = steps.question;
            votingOpen = true;
            tickTime = questionTime;

            ui.setQuestion(qm.getQuestion());
        } else if (currentStep == steps.scores) {
            gameCanvas.enabled = true;
            scoreCanvas.enabled = false;

            currentStep = steps.question;
            votingOpen = true;
            tickTime = questionTime;
            foreach (playerObj p in players)
            {
                p.setAnswer(-1);
            }

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
