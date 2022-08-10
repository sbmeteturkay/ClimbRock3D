using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int level = 0;
    public static int gem = 0;
    private void Awake()
    {
       level= PlayerPrefs.HasKey("level")? GetLevel():SetAndGetLevel(0);
        SetScore(0);
        gem = PlayerPrefs.HasKey("gem") ? GetLevel() : SetAndGetGem(0);
    }
    public static void GemCollect()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.gem);
        SetAndGetGem(GetGem() + 1);
        UIManager.Instance.UpdateGemText();
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
    public static int GetGem()
    {
        return PlayerPrefs.GetInt("gem");
    }
    public static int SetAndGetGem(int i)
    {
        PlayerPrefs.SetInt("gem", i);
        return GetGem();
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
