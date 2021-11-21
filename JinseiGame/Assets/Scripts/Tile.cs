using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class Tile : MonoBehaviourPunCallbacks
{
    public TileInfo tileInfo;
    CourseSelect courseSelect;
    RouletteController rouletteController;
    [SerializeField] GameObject buyHousePanel;
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject moneyShowerPrefab;
    CarMovement stoppingCarMovement;
    
    void Start() {
        courseSelect = GameObject.Find("CourseSelect").GetComponent<CourseSelect>();
        rouletteController = GameObject.Find("Canvas").transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<RouletteController>();
    }
    
    public void Stopped(ref PlayerData playerData){
        stoppingCarMovement = playerData.gameObject.GetComponent<CarMovement>();    //現在止まっているCarmovementを設定
        descriptionText.transform.parent.gameObject.SetActive(true);
        descriptionText.text = tileInfo.description;

        switch(tileInfo.tileType){
            case EnumDefinitions.TileType.START:
                
                break;
            case EnumDefinitions.TileType.MONEY:
                if((playerData.currentMoney + tileInfo.money_delta) < 0){
                    playerData.debt += -1 * (playerData.currentMoney + tileInfo.money_delta);
                    playerData.currentMoney = 0;
                    break;
                }
                if(tileInfo.money_delta > 0){
                    GameObject moneyShower = Instantiate(moneyShowerPrefab,playerData.gameObject.transform.position + new Vector3(-1.9f,10,0.5f),Quaternion.Euler(-90,0,0));
                    ParticleSystem ps = moneyShower.GetComponent<ParticleSystem>();
                    ps.Stop();
                    var main = ps.main;
                    float duration = tileInfo.money_delta / 10000f;
                    if(duration < 1) duration = 1;
                    if(duration > 2) duration = 2;  //値調節
                    main.duration = duration;
                    ps.Play();
                    Destroy(moneyShower,4f);
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
                if(((int)tileInfo.job - 9) == (int)playerData.job){
                    //ランクアップ先が自分の上位互換の職業だったらランクアップ
                    playerData.job = tileInfo.job;
                }else{
                    descriptionText.text = $"「{ConstData.jobName[(int)tileInfo.job]}」にエントリーしていただき誠にありがとうございました。チームで慎重に検討した結果、ご希望に添いかねる形となりました。{playerData.name}様の益々のご活躍をお祈り申し上げます。";
                    playerData.treasures.Add(EnumDefinitions.Treasure.MAIL);    //お祈りメール追加
                }
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

        

        if(tileInfo.isSalaryTile){
            if(playerData.job == EnumDefinitions.Job.GAMBLER || playerData.job == EnumDefinitions.Job.TOP_GAMBLER){
                int baseSalary = ConstData.Salaries[playerData.job];
                StartCoroutine(nameof(DecideGamblerSalary), baseSalary);
            }else{
                int salary = ConstData.Salaries[playerData.job];
                playerData.currentMoney += salary;  //給料追加
                
                GameObject moneyShower = Instantiate(moneyShowerPrefab,playerData.gameObject.transform.position + new Vector3(-1.9f,10,0.5f),Quaternion.Euler(-90,0,0));
                ParticleSystem ps = moneyShower.GetComponent<ParticleSystem>();
                ps.Stop();
                var main = ps.main;
                float duration = ConstData.Salaries[playerData.job] / 10000f;
                if(duration < 1) duration = 1;
                if(duration > 2) duration = 2;  //値調節
                main.duration = duration;
                ps.Play();
                Destroy(moneyShower,4f);

                descriptionText.text += ("$" + salary.ToString() + "だ！");

            }
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

    //stringが参照型だから参照わたしみたいなのをコルーチンで表現できる
    IEnumerator DecideGamblerSalary(int baseSalary){
        rouletteController.transform.parent.parent.gameObject.SetActive(true);
        while(!rouletteController.isRouletteStopped){
            yield return null;
        }
        rouletteController.isRouletteStopped = false;
        rouletteController.transform.parent.parent.gameObject.SetActive(false);
        int result = RouletteController.GetResult();
        int salary = baseSalary * result;
        PlayerData playerData = stoppingCarMovement.GetComponent<PlayerData>();
        playerData.currentMoney += salary;

        var hashtable_player = new ExitGames.Client.Photon.Hashtable();   
        hashtable_player["currentMoney"] = playerData.currentMoney;
        hashtable_player["debt"] = playerData.debt;
        hashtable_player["job"] = ConstData.jobName[(int)playerData.job];
        hashtable_player["familyNum"] = playerData.familyNum;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable_player);

        GameObject moneyShower = Instantiate(moneyShowerPrefab,playerData.gameObject.transform.position + new Vector3(-1.9f,10,0.5f),Quaternion.Euler(-90,0,0));
        ParticleSystem ps = moneyShower.GetComponent<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        float duration = salary / 10000f;
        if(duration < 1) duration = 1;
        if(duration > 2) duration = 2;  //値調節
        main.duration = duration;
        ps.Play();
        Destroy(moneyShower,4f);

        descriptionText.text += ("$" + salary.ToString() + "だ！");
    }
}
