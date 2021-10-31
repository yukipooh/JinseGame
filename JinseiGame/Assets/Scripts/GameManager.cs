using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject startTile;
    public GameObject goalTile;
    public GameObject resultTextForMove;
    public GameObject resultTextForDecide;
    public static Dictionary<int, GameObject> carObjects = new Dictionary<int, GameObject>();
    public static List<CarMovement> carMovements = new List<CarMovement>();
    public static GameObject femalePin;
    public static GameObject childPin;
    
    public List<Player> players;    //ターンに使う

    [SerializeField] WayMaterialChanger wayMaterialChanger;
    [SerializeField] RouletteMaker[] rouletteMakers;
    [SerializeField] ConstData constData;
    [SerializeField] SampleScene sampleScene;
    [SerializeField] Text playerNameText;
    [SerializeField] Text currentMoneyText;
    [SerializeField] GameObject m_femalePin;
    [SerializeField] GameObject m_childPin;
    [SerializeField] TurnManager turnManager;

    

    //順番大事よ
    void Start() {
        constData.Initialize();
        foreach(RouletteMaker rouletteMaker in rouletteMakers){
            rouletteMaker.Initialize();
        }
        wayMaterialChanger.Initialize();
        this.Initialize();
        sampleScene.Initialize();
        turnManager.Initialize(players);
    }

    void Initialize(){
        femalePin = m_femalePin;
        childPin = m_childPin;

        players = new List<Player>(PhotonNetwork.PlayerList);
    }

    public void SetPlayerNameText(string name){
        playerNameText.text = name;
    }

    public void SetCurrentMoneyText(int money){
        currentMoneyText.text = "所持金：" + money.ToString();
    }
}
