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
    public static GameObject femalePin;
    public static GameObject childPin;

    [SerializeField] WayMaterialChanger wayMaterialChanger;
    [SerializeField] RouletteMaker rouletteMaker;
    [SerializeField] ConstData constData;
    [SerializeField] Text playerNameText;
    [SerializeField] Text currentMoneyText;
    [SerializeField] GameObject m_femalePin;
    [SerializeField] GameObject m_childPin;


    //順番大事よ
    void Start() {
        
        constData.Initialize();
        rouletteMaker.Initialize();
        wayMaterialChanger.Initialize();
        this.Initialize();
        
    }

    void Initialize(){
        femalePin = m_femalePin;
        childPin = m_childPin;
    }

    public void SetPlayerNameText(string name){
        playerNameText.text = name;
    }

    public void SetCurrentMoneyText(int money){
        currentMoneyText.text = "所持金：" + money.ToString();
    }
}
