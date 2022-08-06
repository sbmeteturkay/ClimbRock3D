using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValueReferencer : MonoBehaviour
{
    public TextAsset valuesJson;
    [System.Serializable]
    public class Values
    {
        public float obstacleLinearStartSpeed;
        public float handlePositionGap;
        public float climbSpeed;
    }
    [System.Serializable]
    public class LevelList
    {
        public Values[] values;
    }
    public SpawnControl spawnControl;
    public ClimbMechanic climbMechanic;
    public LevelList values=new LevelList();
    // Start is called before the first frame update
    void Awake()
    {
        int level = GameManager.level;
        values = JsonUtility.FromJson<LevelList>(valuesJson.text);
        Debug.Log(values.values[0]);
        spawnControl.handleYpositionGap = values.values[level].handlePositionGap;
        climbMechanic.climbSpeed = values.values[level].climbSpeed;
        ObstacleMovement.startLinearSpeed = values.values[level].obstacleLinearStartSpeed;
    }

}