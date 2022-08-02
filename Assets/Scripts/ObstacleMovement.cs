using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObstacleMovement : MonoBehaviour
{
    [Header("Turn Around")]
    public bool turn = false;
    public float speed = 1f;
    Vector3 rot = new Vector3(0, 360, 0);
    [Header("Linear Movement")]
    public bool linearMovement = false;
    public float linearSpeed = 1f;
    Vector3[] wayPoints;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = new Vector3[3];
        wayPoints.SetValue(new Vector3(2.4f, 0), 1);
        wayPoints.SetValue(new Vector3(0, 0), 0);
        wayPoints.SetValue(new Vector3(-2.4f, 0), 2);
        if (turn)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 360), speed, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental); ;
            // transform.DOMoveX(2.4f,1,false).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
           
        }
        if (linearMovement)
        {
            transform.DOLocalPath(wayPoints, 2).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
