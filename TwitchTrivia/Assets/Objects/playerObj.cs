

public class playerObj {

    string username;
    int score = 0;
    int bouns = 0;
    int fifty = 3;
    int crowd = 3;
    int answer = -1; //-2=crowd
    int inactive = 0;

    public playerObj(string username) { this.username = username; }
    playerObj(string username, int score) {
        this.username = username;
        this.score = score;
    }

    public string getUsername() { return username; }

    public void cashIn() {
        score = score + bouns;
        bouns = 0;
    }

    public void addScore(int points) { score += points;}
    public int getScore() { return score; }

    public void setBonus(int value) { bouns = value; }
    public int getBouns() { return bouns; }
    public void addBouns() {
        if (bouns == 0)
        {
            bouns = 1;
        }
        else
        {
            bouns = bouns * 2;
        }
    }

    public void setInactive() {
        answer = -1;
        inactive += 1;
    }

    public void setAnswer(int a) {
        answer = a;
        inactive = 0;
    }

    public int getAnswer() { return answer; }

    public void addFifty(int amount) {
        fifty += amount;
    }

    public void useFifty() {
        if (fifty > 0)
        {
            addFifty(-1);
            //TODO: send whisper to user.
        }
        else
        {
            //TODO: send whisper saying there are no more fifties.
        }
    }

    public void addCrowd(int amount) {
        crowd += amount;
    }

    public void useCrowd() {
        if (crowd > 0)
        {
            setAnswer(-2);
        }
        else
        {
            //TODO: send whisper saying there are no more crowds.
        }
    }

}
