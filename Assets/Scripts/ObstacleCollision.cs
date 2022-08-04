using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Collided with "+other.name);
            //force push player
            other.attachedRigidbody.AddExplosionForce(50, transform.position, 50f, 70f, ForceMode.Impulse);
            //invoke fail events if player hits obstacle
            UIManager.Instance.failEvents.Invoke();

    }
}
