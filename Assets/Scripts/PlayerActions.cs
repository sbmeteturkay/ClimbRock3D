using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerActions Instance;
    public GameObject hand;
    public UIManager manager;
    PlayerActions(){
        Debug.Log("deneme");
}
    CharacterJoint[] joints;
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        joints = GetComponentsInChildren<CharacterJoint>();
    }
    public void DeleteJoints()
    {
        foreach (CharacterJoint rb in joints)
        {
            rb.connectedBody = null;
        }
    }
    public void ReleaseHand()
    {
        Destroy(hand.GetComponent<ConfigurableJoint>());
        manager.OpenFailedPanel();
    }
}
