using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startTile;
    public GameObject goalTile;
    public static Dictionary<int, GameObject> carObjects = new Dictionary<int, GameObject>();
    public static List<CarMovement> carMovements = new List<CarMovement>();

    [SerializeField] WayMaterialChanger wayMaterialChanger;
    [SerializeField] RouletteMaker rouletteMaker;
    [SerializeField] ConstData constData;
    [SerializeField] Text playerNameText;
    [SerializeField] Text currentMoneyText;

    //順番大事よ
    void Start() {
        
        constData.Initialize();
        rouletteMaker.Initialize();
        wayMaterialChanger.Initialize();
        
        
    }

    public void SetPlayerNameText(string name){
        playerNameText.text = name;
    }

    public void SetCurrentMoneyText(int money){
        currentMoneyText.text = "所持金：" + money.ToString();
    }
}
