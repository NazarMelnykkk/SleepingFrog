using UnityEngine;

public class ScoreStorage : MonoBehaviour
{
    private static int _score = 0;


    public static int GetScoreValue()
    {
        return _score;
    }

    public static void SetScore(int score)
    {
        _score = score;
    }


    public static void ReseScore()
    {
        _score = 0;
    }
}
