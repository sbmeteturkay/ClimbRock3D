using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int level = 0;
    private void Start()
    {
       level= PlayerPrefs.HasKey("level")? GetLevel():SetAndGetLevel(0);
        SetScore(0);
    }
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("level");
    }
    public static int SetAndGetLevel(int i)
    {
        PlayerPrefs.SetInt("level", i);
        return GetLevel();
    }

    public static int GetScore()
    {
        return score;
    }
    public static void SetScore(int i)
    {
        score = i;
    }
    
}
