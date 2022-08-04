using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;

    public static int GetScore()
    {
        return score;
    }
    public static void SetScore(int i)
    {
        score = i;
    }
}
