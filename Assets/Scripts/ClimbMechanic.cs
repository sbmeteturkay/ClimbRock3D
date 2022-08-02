using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ClimbMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cameraMain;
    public GameObject parentPlayer,rightHand, leftHand;
    private FixedJoint rightJoint, leftJoint;
    public Rigidbody[] Rigidbodies;
    public static int currentHandleIndex = 0;
    void Start()
    {
        cameraMain = Camera.main;
        Rigidbodies = parentPlayer.GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                if (hit.transform.gameObject.tag == "Handle")
                {
                    AttachHandle(hit);
                    currentHandleIndex++;
                }
            }
        }

        
    }
    private void AttachHandle(RaycastHit hit)
    {
        //CloseRigidbodys();
        //  CloseRigidbodyRotations();
        //rightHand.transform.position = hit.transform.position;
        //if(Random.Range(0, 2)!=0){
            if (rightJoint != null)
            {
               // rightJoint.connectedBody = hit.transform.gameObject.GetComponent<Rigidbody>();
                rightHand.transform.DOMove(hit.transform.position, 0.5f);

            }
            else
            {
             //   rightHand.AddComponent<FixedJoint>().connectedBody = hit.transform.gameObject.GetComponent<Rigidbody>();
                rightJoint = rightHand.GetComponent<FixedJoint>();
                rightHand.transform.DOMove(hit.transform.position, 0.5f);
                //rightHand.transform.position = hit.transform.position;
            }
        //}
        //else
        //{
        //    if (leftJoint != null)
        //    {
        //        leftJoint.connectedBody = hit.transform.gameObject.GetComponent<Rigidbody>();
        //        leftHand.transform.DOMove(hit.transform.position, 0.5f);

        //    }
        //    else
        //    {
        //        leftHand.AddComponent<FixedJoint>().connectedBody = hit.transform.gameObject.GetComponent<Rigidbody>();
        //        leftJoint = leftHand.GetComponent<FixedJoint>();
        //        leftHand.transform.DOMove(hit.transform.position, 0.5f);
        //        //rightHand.transform.position = hit.transform.position;
        //    }
        //}

        
        //OpenRigidbodys();
        //OpenRigidbodyRotations();
    }
    
    private void CloseRigidbodys() {
        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.isKinematic = true;
        }
    }
    private void OpenRigidbodys()
    {
        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
    private void CloseRigidbodyRotations()
    {
        foreach (Rigidbody rb in Rigidbodies)
        {
            if (rb.gameObject.tag != "Hand")
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
    private void OpenRigidbodyRotations()
    {
        foreach (Rigidbody rb in Rigidbodies)
        {
            if (rb.gameObject.tag != "Hand")
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
    //IEnumerable ScaleMe(Transform objTr)
    //{
    //    objTr.localScale *= 1.2f;
    //    yield return new WaitForSeconds(0.5f);
    //    objTr.localScale /= 1.2f;
    //}
}
