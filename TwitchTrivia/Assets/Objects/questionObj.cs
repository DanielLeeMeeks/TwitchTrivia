
[System.Serializable]
public class questionObj {

    public string category;
    public string type;
    public string difficulty;
    public string question;
    public string correct_answer;
    public string[] incorrect_answers;

    public string[] answers = new string[4];
    public int correct_answer_id;
    public string[] answers_letters = new string[4];

    public void setAnswers() {

        if (type == "boolean")
        {
            answers[0] = "";
            answers[1] = "True";
            answers[2] = "";
            answers[3] = "False";

            answers_letters[0] = "";
            answers_letters[1] = "T";
            answers_letters[2] = "";
            answers_letters[3] = "F";

            if (correct_answer == "True")
            {
                correct_answer_id = 2;
            }
            else
            {
                correct_answer_id = 3;
            }
        }
        else
        {
            correct_answer_id = UnityEngine.Random.Range(0, 4);

            int ic_count = 0;
            int count = 0;

            //UnityEngine.Debug.Log(incorrect_answers.Length);

            answers_letters[0] = "A";
            answers_letters[1] = "B";
            answers_letters[2] = "C";
            answers_letters[3] = "D";

            while (count < incorrect_answers.Length + 1)
            {
                if (correct_answer_id == count)
                {
                    answers[count] = correct_answer;
                }
                else
                {
                    answers[count] = incorrect_answers[ic_count];
                    ic_count++;
                }
                count++;
                //UnityEngine.Debug.Log(answers[count] + ", " + incorrect_answers[ic_count]);
            }

            //UnityEngine.Debug.Log(answers[0] + ", " + answers[1] + ", " + answers[2] + ", " + answers[3]);
        }
    }

}
