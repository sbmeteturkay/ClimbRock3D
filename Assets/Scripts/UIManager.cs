using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header ("Trigger when player failed")]
    public UnityEvent failEvents;
    [Header("Trigger when player win")]
    public UnityEvent winEvents;
    [Header("In game ui references")]
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text scoreFinal;
    [SerializeField] TMP_Text gem;

    // Start is called before the first frame update
    private void Start()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion
        UpdateGemText();
    }
    public void UpdateScoreText()
    {
        score.text = "Score: "+GameManager.GetScore().ToString();
        scoreFinal.text = score.text;
    }
    public void UpdateGemText()
    {
        gem.text = GameManager.GetGem().ToString();
    }
}
