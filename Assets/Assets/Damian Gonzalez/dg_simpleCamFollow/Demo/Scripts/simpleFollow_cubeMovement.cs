using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleFollow_cubeMovement : MonoBehaviour
{
    Rigidbody rb;
    float lastJump = 0;
    dg_simpleCamFollow followScript;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        followScript = Camera.main.GetComponent<dg_simpleCamFollow>();
        if (!followScript.takeOffsetFromInitialPos) SetPreset(1);
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (Mathf.Abs(x) > 0.1f) {
            rb.velocity = new Vector3(
                x * 7f,
                rb.velocity.y,
                rb.velocity.z
            );
        }

        if (Time.time > lastJump + 1.2f && (Input.GetAxis("Vertical") > .05f || Input.GetButtonDown("Jump"))) {
            rb.AddForce(0, 8f, 0, ForceMode.Impulse);
            lastJump = Time.time;
        }

        //toogle "look at" boolean
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) SetPreset(1);
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) SetPreset(2);
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) SetPreset(3);
        if (Input.GetKeyDown(KeyCode.L)) {
            followScript.lookAtTarget = !followScript.lookAtTarget;

            //stop looking (rotation back to none)
            if (!followScript.lookAtTarget) Camera.main.transform.rotation = Quaternion.identity; 
        }
    }

    public void SetPreset(int i) {
        switch (i) {
            case 1:
                followScript.generalOffset = new Vector3(0, 0.2f, -10);
                followScript.lookAtTarget = false;
                followScript.laziness = 10;
                Camera.main.orthographic = false;
                Camera.main.transform.rotation = Quaternion.identity;
                break;

            case 2:
                followScript.generalOffset = new Vector3(0, .8f, -6);
                followScript.lookAtTarget = true;
                followScript.laziness = 40;
                Camera.main.orthographic = false;
                break;

            case 3:
                followScript.generalOffset = new Vector3(0, 0, -15);
                followScript.lookAtTarget = false;
                followScript.laziness = 20;
                Camera.main.orthographic = true;
                Camera.main.transform.rotation = Quaternion.identity;
                break;
        }

    }

}
