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
    [SerializeField] GameObject resultPanel;    //ゴール後のリザルト画面
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject moneyShowerPrefab;
    CarMovement stoppingCarMovement;
    bool isAllGoaled = false;
    
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
                    GameObject icon = GameObject.Find("PlayerIcon");
                    icon.transform.GetChild(icon.transform.childCount - playerData.familyNum).gameObject.SetActive(true);   //iconに子供表示
                    stoppingCarMovement.ShowPin(true, false);
                }
                break;
            case EnumDefinitions.TileType.BIRTH:
                playerData.familyNum++; //子供追加
                GameObject playerIcon = GameObject.Find("PlayerIcon");
                playerIcon.transform.GetChild(playerIcon.transform.childCount - playerData.familyNum).gameObject.SetActive(true);   //iconに子供表示
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
                GameObject treasurePanelObject = GameObject.Find("TreasurePanel");
                Debug.Log(treasurePanelObject);
                TreasurePanel treasurePanel = treasurePanelObject.GetComponent<TreasurePanel>();
                treasurePanel.AddTreasure(tileInfo.treasure, playerData.treasures.Count);
                playerData.treasures.Add(tileInfo.treasure);
                playerData.currentMoney += tileInfo.money_delta;
                break;
            case EnumDefinitions.TileType.HOUSING:
                buyHousePanel.SetActive(true);
                break;
            case EnumDefinitions.TileType.INSURANCE:
                playerData.insurances.Add(tileInfo.insurance);  //保険追加
                break;
            case EnumDefinitions.TileType.SETTLE:
                int totalDebt = (int)(playerData.debt * 1.25f);
                if(playerData.currentMoney - totalDebt >= 0){
                    //借金を返せるなら
                    playerData.currentMoney -= totalDebt;
                    playerData.debt = 0;
                    descriptionText.text = $"ここは決算マス。あなたのこれまでの借金を清算するマスです。あなたは{playerData.debt}の借金をしていたため、支払額は1.25倍の${(int)(playerData.debt * 1.25f)}となります。返済ありがとうございました。";
                }else{
                    //借金返せなかったら5秒間のクリック数×$500労働
                    playerData.debt -= playerData.currentMoney;
                    playerData.currentMoney = 0;
                    var hashtable_settle = new ExitGames.Client.Photon.Hashtable();   
                    hashtable_settle["isCanPayDebt"] = false;
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable_settle);
                    GameObject.Find("UIManager").GetComponent<UIManager>().ShowSettlePanel();
                }
                break;
            case EnumDefinitions.TileType.GOAL:
                playerData.isGoaled = true;
                StartCoroutine(nameof(CheckIsAllGoaledAndShowResult));
                
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
        PressEnterText pressEnterText = GameObject.Find("PressEnterAlert").GetComponent<PressEnterText>();
        pressEnterText.StartCoroutine("AlertAnimation");
        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }
        PressEnterText.isPressedEnter = true;
        carMovement.StartCoroutine(carMovement.Dice(false,transform.parent.childCount - transform.GetSiblingIndex()));
    }

    /// <summary>
    /// プレイヤーがゴールした際に全員がゴールしたかどうかチェックしisAllGoaledを更新する
    /// 全員ゴールしていたらリザルト表示
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckIsAllGoaledAndShowResult(){
        var goalHash = new ExitGames.Client.Photon.Hashtable();
        goalHash["isGoaled"] = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(goalHash);
        yield return new WaitForSeconds(1); //カスタムプロパティが正常に登録されるのを待つ
        isAllGoaled = true; //一旦trueにしておく
        foreach(Player player in PhotonNetwork.PlayerList){
            if((bool)player.CustomProperties["isGoaled"] == false){
                isAllGoaled = false;    //一人でもゴールしていなかったらfalseに
            }
        }
        //全員ゴールしていたら
        if(isAllGoaled){
            //result表示
            photonView.RPC(nameof(ShowResult),RpcTarget.AllBuffered);
        }
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
        if(CarMovement.isJustStopped){
            stoppingCarMovement.TurnEnd();  //ターンエンド
            CarMovement.isJustStopped = false;
        }
    }

    [PunRPC]
    async void ShowResult(){
        
        PlayerData playerData = null;
        DateTime update = GameManager.updateTime;
        MoneyRateFromUSDObject moneyObject = GameManager.moneyObject;
        resultPanel.SetActive(true);
        resultPanel.transform.GetChild(0).GetComponent<Text>().text = $"(最終情報取得 : {update.Year}年{update.Month}月{update.Day}日{update.Hour}時{update.Minute}分)";
        resultPanel.transform.GetChild(1).GetComponent<Text>().text = $"$1 = ${moneyObject.JPY}円";
        
        foreach(CarMovement carMovement in GameManager.carMovements){
            if(carMovement.photonView.IsMine){
                playerData = carMovement.gameObject.GetComponent<PlayerData>();
            }
        }

        resultPanel.transform.GetChild(2).GetComponent<Text>().text = $"あなたの所持金は${playerData.currentMoney}!\n円換算すると{(int)(playerData.currentMoney * moneyObject.JPY)}円です！！！";

        List<PlayerMoneyData> playerMoneyDatas = new List<PlayerMoneyData>(PhotonNetwork.PlayerList.Length);
        foreach(Player player in PhotonNetwork.PlayerList){
            PlayerMoneyData playerMoneyData = new PlayerMoneyData();
            playerMoneyData.playerName = player.NickName;
            playerMoneyData.playerMoney = (int)player.CustomProperties["currentMoney"];
            playerMoneyDatas.Add(playerMoneyData);
        }
        playerMoneyDatas.Sort((a,b) => (b.playerMoney - a.playerMoney));    //降順でソート
        GameObject placementPanel = resultPanel.transform.GetChild(3).gameObject;
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++){
            placementPanel.transform.GetChild(i).GetComponent<Text>().text = $"{i + 1}位 : {playerMoneyDatas[i].playerName} {(int)(playerMoneyDatas[i].playerMoney * moneyObject.JPY)}円";
            placementPanel.transform.GetChild(i).gameObject.SetActive(true);
        }
        // foreach(PlayerMoneyData item in playerMoneyDatas){
        //     Debug.Log($"PlayerName : {item.playerName}, PlayerMoney : {item.playerMoney}");
        // }
    }
}

/// <summary>
/// ゴール後の順位比較のためのデータ
/// </summary>
public class PlayerMoneyData{
    public string playerName;  //プレイヤーの名前
    public int playerMoney;    //プレイヤーの最終的な所持金
}
