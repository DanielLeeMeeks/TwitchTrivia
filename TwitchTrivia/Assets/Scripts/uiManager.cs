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
    string footerText;

    // Use this for initialization
    void Start () {
		
	}

    public void setQuestion(questionObj q) {
        question.text = q.question;
        if (q.type == "boolean")
        {
            answerBackground[0].enabled = false;
            answerBackground[1].enabled = true;
            answerBackground[2].enabled = false;
            answerBackground[3].enabled = true;
            footerText = "To submit your answer, type #T or #F in the chat.\nTo use 50 / 50, type #5050 in chat.  To trust the crowd, type #crowd in chat.";
        }
        else
        {
            answerBackground[0].enabled = true;
            answerBackground[1].enabled = true;
            answerBackground[2].enabled = true;
            answerBackground[3].enabled = true;
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
