using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Tile : MonoBehaviourPunCallbacks
{
    public TileInfo tileInfo;
    CourseSelect courseSelect;
    [SerializeField] GameObject buyHousePanel;
    [SerializeField] Text descriptionText;
    CarMovement stoppingCarMovement;
    
    void Start() {
        courseSelect = GameObject.Find("CourseSelect").GetComponent<CourseSelect>();
    }
    
    public void Stopped(ref PlayerData playerData){
        stoppingCarMovement = playerData.gameObject.GetComponent<CarMovement>();    //現在止まっているCarmovementを設定
        switch(tileInfo.tileType){
            case EnumDefinitions.TileType.START:
                
                break;
            case EnumDefinitions.TileType.MONEY:
                if((playerData.currentMoney + tileInfo.money_delta) < 0){
                    playerData.debt += -1 * (playerData.currentMoney + tileInfo.money_delta);
                    playerData.currentMoney = 0;
                    break;
                }
                playerData.currentMoney += tileInfo.money_delta;
                break;
            case EnumDefinitions.TileType.EMPLOY:
                playerData.job = tileInfo.job;
                
                if(tileInfo.isMoveToNextCourseTile){
                    StartCoroutine(MoveToNextCourseTile(stoppingCarMovement));
                }
                break;
            case EnumDefinitions.TileType.JOB_RANKUP:
                playerData.job = tileInfo.job;
                break;
            case EnumDefinitions.TileType.MARRY:
                if(playerData.familyNum == 1){
                    playerData.familyNum++; //配偶者を追加
                    stoppingCarMovement.ShowPin(true, false);
                }
                break;
            case EnumDefinitions.TileType.BIRTH:
                playerData.familyNum++; //子供追加
                switch(playerData.familyNum){
                    case 3:
                        stoppingCarMovement.ShowPin(false, true, 0);
                        break;
                    case 4:
                        stoppingCarMovement.ShowPin(false, true, 1);
                        break;
                    case 5:
                        stoppingCarMovement.ShowPin(false, true, 2);
                        break;
                    case 6:
                        stoppingCarMovement.ShowPin(false, true, 3);
                        break;
                }
                break;
            case EnumDefinitions.TileType.TREASURE:
                playerData.treasures.Add(tileInfo.treasure);
                playerData.currentMoney -= tileInfo.money_delta;
                break;
            case EnumDefinitions.TileType.HOUSING:
                buyHousePanel.SetActive(true);
                break;
            case EnumDefinitions.TileType.INSURANCE:
                playerData.insurances.Add(tileInfo.insurance);  //保険追加
                break;
            case EnumDefinitions.TileType.SETTLE:
                break;
            case EnumDefinitions.TileType.GOAL:
                playerData.isGoaled = true;
                break;
            case EnumDefinitions.TileType.BRANCH:
                if(gameObject.name == "0031_branch"){
                    //天国コースと地獄コースへの分岐マス
                    courseSelect.ShowRoulette();
                }
                if(gameObject.name == "0073_branchB"){
                    courseSelect.ShowPanel(2);
                }
                
                break;
        }

        descriptionText.transform.parent.gameObject.SetActive(true);
        descriptionText.text = tileInfo.description;

        if(tileInfo.isSalaryTile){
            playerData.currentMoney += ConstData.Salaries[playerData.job];  //給料追加
            descriptionText.text += (ConstData.Salaries[playerData.job].ToString() + "$だ！");
        }
        
        var hashtable_player = new ExitGames.Client.Photon.Hashtable();   
        hashtable_player["currentMoney"] = playerData.currentMoney;
        hashtable_player["debt"] = playerData.debt;
        hashtable_player["job"] = ConstData.jobName[(int)playerData.job];
        hashtable_player["familyNum"] = playerData.familyNum;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable_player);

        var hashtable_room = new ExitGames.Client.Photon.Hashtable();
        hashtable_room["description"] = descriptionText.text;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable_room);

    }

    //このコルーチン　isRedのマスに適用するとDiceが重複してバグるから注意
    IEnumerator MoveToNextCourseTile(CarMovement carMovement){
        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }
        carMovement.StartCoroutine(carMovement.Dice(false,transform.parent.childCount - transform.GetSiblingIndex()));
    }
}
