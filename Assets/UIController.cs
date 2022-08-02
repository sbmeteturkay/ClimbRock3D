using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text score;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + (ClimbMechanic.currentHandleIndex+1).ToString();
    }
}
