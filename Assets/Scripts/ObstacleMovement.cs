using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObstacleMovement : MonoBehaviour
{
    [Header("Turn Around")]
    [SerializeField] bool turn = false;
    [SerializeField] float speed = 1f;
    [Header("Linear Movement")]
    [SerializeField] bool linearMovement = false;
    float linearSpeed = 1f;
    public static float startLinearSpeed=1;
    static Vector3[] wayPoints;
    [SerializeField] bool setSpeedOwn = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!setSpeedOwn)
            linearSpeed = startLinearSpeed;
        if (wayPoints== null)
        {
            wayPoints = new Vector3[3];
            wayPoints.SetValue(new Vector3(transform.localPosition.x+2.6f, 0), 0);
            wayPoints.SetValue(new Vector3(transform.localPosition.x-2.6f, 0), 1);
        }
        if (turn)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 360), 1f/speed, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental); ;
            // transform.DOMoveX(2.4f,1,false).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        if (linearMovement)
        {
            transform.DOLocalPath(wayPoints, 10/linearSpeed).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }
}
