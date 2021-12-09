using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Text score;
    [SerializeField] Text highScoreText;

    private float currentScore;

    private static float highScore;
    // Update is called once per frame

    void Start() {
        //get the saved high score from PlayerPrefs
        highScore=PlayerPrefs.GetFloat("HighScore");
        print(highScore);
        //set UI text to players high score
        highScoreText.text="High Score: "+highScore.ToString("0");
    }   

    void Update()
    {   
        SetScore();
    }
    
    void SetScore()
    {
        
        if(player!=null)
        {   
            currentScore=player.position.z;
            //set the players score to their z position
            //save to PlayerPrefs
            PlayerPrefs.SetFloat("Score",currentScore);

            //convert players score into string with no decimals and 
            //set the UI text to the score
            score.text="Score: "+currentScore.ToString("0");


            //set highScore if player beats it
            if(currentScore>highScore)
            {   
                highScore=currentScore;
                SetHighScore(currentScore);
                
            }
        }
    }

    void SetHighScore(float highscore)
    {
        if(player!=null)
        {
            PlayerPrefs.SetFloat("HighScore",highscore);
            highScoreText.text="High Score: "+PlayerPrefs.GetFloat("HighScore").ToString("0");
        }
    }
}
