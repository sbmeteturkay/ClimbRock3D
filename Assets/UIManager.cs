using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject failed;
    // Start is called before the first frame update
    public void OpenFailedPanel()
    {
        failed.SetActive(true);
    }
}
