using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] UIManager manager;
    CharacterJoint[] joints;
    void Start()
    {
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
        //Release players grabbing hand to fall of
        Destroy(hand.GetComponent<ConfigurableJoint>());
        manager.OpenFailedPanel();
    }

}
