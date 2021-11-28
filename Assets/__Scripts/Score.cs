using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Text score;
    [SerializeField] Text highScoreText;

    private string storedScore="";

    private static float highScore;
    // Update is called once per frame

    void Start() {
        highScoreText.text="High Score: "+PlayerPrefs.GetFloat("HighScore").ToString("0");
        highScore=PlayerPrefs.GetFloat("HighScore");
    }   

    void Update()
    {   
        SetScore();
    }
    
    void SetScore()
    {
        if(player!=null)
        {   
            PlayerPrefs.SetFloat("Score",player.position.z);
            storedScore=score.text="Score: "+player.position.z.ToString("0");
            if(player.position.z>highScore)
            {
                highScore=player.position.z;
                SetHighScore(player.position.z);
                
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

    public float getScore(){
        if(player!=null){
            return player.position.z;
        }
        return 0;
    }
}
