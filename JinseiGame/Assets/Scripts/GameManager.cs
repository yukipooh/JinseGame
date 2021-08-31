using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playerCars = new List<GameObject>();
    public GameObject startTile;
    public GameObject goalTile;


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
