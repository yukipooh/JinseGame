using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startTile;
    public GameObject goalTile;
    public static Dictionary<int, GameObject> carObjects = new Dictionary<int, GameObject>();


    [SerializeField] WayMaterialChanger wayMaterialChanger;
    [SerializeField] RouletteMaker rouletteMaker;
    [SerializeField] CarMovement carMovement;
    [SerializeField] ConstData constData;

    //順番大事よ
    void Start() {
        
        constData.Initialize();
        carMovement.Initialize();
        rouletteMaker.Initialize();
        wayMaterialChanger.Initialize();
        
        
    }
}
