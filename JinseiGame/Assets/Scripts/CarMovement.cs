using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;

public class CarMovement : MonoBehaviourPunCallbacks
{
    public GameObject roulette;   //RoulettePrefab
    public GameObject femalePin;
    public GameObject[] childPins;

    Text resultText; // resultText
    Text descriptionText;  //description
    GameManager gameManager;
    TurnManager turnManager;
    PlayerData playerData;
    NavMeshAgent navMeshAgent;
    
    Tween tween;
    Vector3 latestPosition;
    int currentNum = 0; //現在いるタイル
    public GameObject currentCourse;    //現在いるコース(オブジェクト)
    public EnumDefinitions.Course currentCourseEnum;  //現在いるコース(enum)
    Rigidbody rigidbody;
    const float INTERVAL = 0.6f;    //次のマスに進むまでに待つ時間

    public bool isStopping = false;    //車が一時停止しているかどうかのフラグ

    const float ROTATE_FORWARD_ADJUST_ANGLE = 90;

    // public void Initialize(){
        
        
    // }

    private void Awake() {
        latestPosition = transform.position;    
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        roulette = GameObject.Find("Roulette_Components");
        resultText = gameManager.resultTextForMove.GetComponent<Text>();
        descriptionText = GameObject.Find("descriptionText").GetComponent<Text>();

        currentCourse = ConstData.Courses[EnumDefinitions.Course.START];
        currentCourseEnum = EnumDefinitions.Course.START;

        GameManager.carMovements.Add(this);
        photonView.name = PhotonNetwork.NickName;
        gameManager.SetPlayerNameText(PhotonNetwork.NickName);
    }

    public void MoveForward(int tileNum){
        descriptionText.transform.parent.gameObject.SetActive(false);
        if(tileNum < currentCourse.transform.childCount){
            navMeshAgent.SetDestination(currentCourse.transform.GetChild(tileNum).transform.position + new Vector3(0,2.8f,0));
            this.currentNum++;
        }
        if(tileNum == currentCourse.transform.childCount){
            Debug.Log("コース移動");
            //次のコースにうつろうとしているとき
            currentCourseEnum = (EnumDefinitions.Course)Enum.ToObject(
                typeof(EnumDefinitions.Course),ConstData.CourseLink[(int)currentCourseEnum]);     //コース移動
            currentCourse = ConstData.Courses[currentCourseEnum];   //オブジェクトも変更しとく
            currentNum = 0; //タイルのインデックスを初期化
            
            navMeshAgent.SetDestination(currentCourse.transform.GetChild(0).transform.position + new Vector3(0,2.8f,0));    //コースのスタートに移動

        }
    }
    
    public IEnumerator Dice(bool isUseRoulette = true, int moveNum = 0){
        int dice;
        if(isUseRoulette){
            dice = int.Parse(RouletteController.result);   //ルーレット回すよ
        }else{
            dice = moveNum;
        }
        Debug.Log(dice);
        for(int i = 0; i < dice; i++){
            MoveForward(currentNum + 1);
            yield return new WaitForSeconds(INTERVAL);
            Debug.Log(currentNum);
            Tile currentTile = currentCourse.transform.GetChild(currentNum).GetComponent<Tile>();
            if(currentTile.tileInfo.isRed){
                if(i != dice - 1){
                    //赤マスにちょうど止まらないとき
                    currentTile.Stopped(ref playerData);    //通り過ぎたマスの効果を発揮
                    // gameManager.SetCurrentMoneyText(playerData.currentMoney);
                }else{
                    break;  //これでちょうど止まった時にブランチ選択の部分でバグらないように
                }
                isStopping = true;  //ブランチを選ぶタイミングでfalseに変える
                if(currentTile.tileInfo.tileType == EnumDefinitions.TileType.BRANCH){
                    while(isStopping){
                        yield return null; 
                    }
                }else{
                    //まだ移動できるとき
                    if(i != dice - 1){
                        while(!Input.GetKeyDown(KeyCode.Return)){
                            yield return null;
                        }
                    }
                }
            
            }
            if(currentTile.tileInfo.isMustStop){
                break;  //移動をやめる
            }
            resultText.text = (dice-i-1).ToString();
        }
        OnMoveEnd();    //移動し終わった
        if(currentCourse.transform.GetChild(currentNum).GetComponent<Tile>().tileInfo.isMoveToNextCourseTile == false){
            //Enterで移動する場合はルーレットを表示しない
            roulette.SetActive(true);
            TurnEnd();
        }
    }

    void OnMoveEnd(){
        //移動し終わった
        currentCourse.transform.GetChild(currentNum).GetComponent<Tile>().Stopped(ref playerData);
        resultText.gameObject.SetActive(false);
        descriptionText.transform.parent.gameObject.SetActive(true);
        // gameManager.SetCurrentMoneyText(playerData.currentMoney);
    }

    public void TurnEnd(){
        photonView.RPC(nameof(RPC_TurnEnd), RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnEnd(){
        turnManager.MoveToNextTurn();
        Debug.Log(turnManager.GetCurrentTurnPlayer().NickName);
    }

    [PunRPC]
    void RPC_ShowPin(bool femalePin, bool childPin, int index = 0){
        if(femalePin){
            this.femalePin.SetActive(true);
        }
        if(childPin){
            this.childPins[index].SetActive(true);
        }
    }

    public void ShowPin(bool femalePin, bool childPin, int index = 0){
        photonView.RPC(nameof(RPC_ShowPin), RpcTarget.All, femalePin, childPin, index);
    }

    void Update() {
        
    }
}
