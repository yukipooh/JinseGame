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


    void Start() {
        rouletteMaker.Initialize();
        wayMaterialChanger.Initialize();
    }
}
