using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject failed;
    [Header ("Trigger when player failed")]
    public UnityEvent failEvents;
    [Header("In game ui references")]
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text scoreFinal;
    // Start is called before the first frame update
    public void OpenFailedPanel()
    {
        failed.SetActive(true);
    }
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

    }
    public void UpdateScoreText()
    {
        score.text = "Score: "+GameManager.GetScore().ToString();
        scoreFinal.text = score.text;
    }
}
