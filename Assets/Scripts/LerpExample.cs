using UnityEngine;

/// <summary>
/// Simple example - add this to an object in the scene, hit the space bar.  It will move vertically 5 meters.
/// </summary>
public class LerpExample : MonoBehaviour
{
    [SerializeField] Rigidbody rb;  // You can reference in inspector or get it however
    [SerializeField] float lerpTime = 1f;   // Time it takes to get to the position - acts a speed
    public Rigidbody[] Rigidbodies;
    float currentLerpTime;
    bool isLerping;
    Vector3 startPos;
    Vector3 endPos;
    public GameObject parentPlayer;
    bool rbClose = false;

    void Awake()
    {
        startPos = transform.position;  // Our current position.  You can update this however.  just examples.
        endPos = transform.position + transform.up * 5f;    // I just made this up, it's wherever you want the object to go
        Rigidbodies = parentPlayer.GetComponentsInChildren<Rigidbody>();
    }


    void FixedUpdate()
    {
        //reset when we press spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLerpTime = 0f;
            isLerping = true;
        }

        if (isLerping)
        {
            if (!rbClose)
            {
               CloseRigidbodys();
            }
            Vector3 movePosition = Vector3.Slerp(transform.position, endPos, 1f * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);
            isLerping = Vector3.Distance(transform.position, endPos) <= 0.01 ? false : true;

        }
        else if(rbClose)
        {
            OpenRigidbodys();
        }
        
    }
    private void CloseRigidbodys()
    {
        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.isKinematic = true;
        }
        rbClose = true;
    }
    private void OpenRigidbodys()
    {
        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.isKinematic = false;
        }
        rbClose = false;
    }
}