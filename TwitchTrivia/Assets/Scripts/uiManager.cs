using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiManager : MonoBehaviour {

    public TextMeshProUGUI question;
    public TextMeshProUGUI footer;
    public TextMeshProUGUI [] answerText;
    public Image [] answerBackground;
    public Image[] answerPrecent;
    string footerText;

    // Use this for initialization
    void Start () {
		
	}

    public void addToFooter(string text) {
        footer.text = "<b>"+ text +  "</b>\n" + footerText;
    }
    public void replaceFooter(string text) { footer.text = text; }

    public void showCorrectAnswer(questionObj q) {
        answerBackground[0].color = Color.red;
        answerBackground[1].color = Color.red;
        answerBackground[2].color = Color.red;
        answerBackground[3].color = Color.red;
        answerBackground[q.correct_answer_id].color = Color.green;
        answerText[q.correct_answer_id].color = Color.black;
        footerText = "The correct answer is \""+q.answers_letters[q.correct_answer_id]+". "+q.answers[q.correct_answer_id]+"\"";
        footer.text = footerText;
    }

    public void setQuestion(questionObj q) {
        question.text = q.question;

        answerPrecent[0].gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
        answerPrecent[1].gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
        answerPrecent[2].gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
        answerPrecent[3].gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);

        if (q.type == "boolean")
        {
            answerBackground[0].enabled = false;
            answerBackground[1].enabled = true;
            answerBackground[2].enabled = false;
            answerBackground[3].enabled = true;

            answerPrecent[0].enabled = false;
            answerPrecent[1].enabled = true;
            answerPrecent[2].enabled = false;
            answerPrecent[3].enabled = true;

            footerText = "To submit your answer, type #T or #F in the chat.\nTo use 50 / 50, type #5050 in chat.  To trust the crowd, type #crowd in chat.";
        }
        else
        {
            answerBackground[0].enabled = true;
            answerBackground[1].enabled = true;
            answerBackground[2].enabled = true;
            answerBackground[3].enabled = true;

            answerPrecent[0].enabled = true;
            answerPrecent[1].enabled = true;
            answerPrecent[2].enabled = true;
            answerPrecent[3].enabled = true;

            footerText = "To submit your answer, type #A, #B, #C, or #D in the chat.\nTo use 50 / 50, type #5050 in chat.  To trust the crowd, type #crowd in chat.";
        }

        int i = 0;
        while (i < 4)
        {
            answerText[i].text = "<b>" + q.answers_letters[i] + "</b>. " + q.answers[i];
            answerText[i].color = Color.white;
            answerBackground[i].color = Color.blue;
            i++;
        }
        footer.text = "<b>Submit your answer now. (30)</b>\n"+footerText;
        Debug.Log(q.correct_answer_id);
    }
	

}
