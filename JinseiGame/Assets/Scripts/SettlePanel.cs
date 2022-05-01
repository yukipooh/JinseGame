using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SettlePanel : MonoBehaviourPunCallbacks
{
    int reward = 0; //報酬
    int timeLimit = 5;  //5秒間でクリック
    bool isFirstClick = true;
    bool isInTime = false;  //制限時間ないかどうかのフラグ
    [SerializeField] Text rewardText;
    [SerializeField] Text timeLimitText;
    [SerializeField] Text resultText;
    [SerializeField] GameObject resultPanel;

    public void InitializePanel(){
        isFirstClick = true;
        isInTime = false;
        resultPanel.SetActive(false);
        reward = 0;
        rewardText.text = "$0";
        timeLimitText.text = "残り:5秒";
    }

    public void OnClickRewardButton(){
        if(isFirstClick){
            isInTime = true;
            isFirstClick = false;
            StartCoroutine(nameof(RewardCount));
        }
        if(isInTime){
            reward += 500;
            rewardText.text = $"${reward}";
        }
    }

    IEnumerator RewardCount(){
        for(int i = 0; i < timeLimit; i++){
            yield return new WaitForSeconds(1);
            timeLimitText.text = $"残り:{timeLimit - i - 1}秒";
        }
        ClickTimeEnd();
    }

    void ClickTimeEnd(){
        isInTime = false;
        resultPanel.SetActive(true);
        resultText.text = $"あなたの今回の労働の報酬は${reward}です。お疲れさまでした。";
        Debug.Log($"賞金は{reward}");
    }

    public void OnClickTurnEndButton(){
        foreach(CarMovement carMovement in GameManager.carMovements){
            if(carMovement.photonView.IsMine){
                if(carMovement.playerData.debt - reward >= 0){
                    //借金が残っていたら
                    carMovement.playerData.debt -= reward;
                }else{
                    carMovement.playerData.debt = 0;
                    carMovement.playerData.currentMoney += (reward - carMovement.playerData.debt);
                    var hashtable_settle = new ExitGames.Client.Photon.Hashtable();   
                    hashtable_settle["isCanPayDebt"] = true;
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable_settle);
                }
                
                var hashtable_player = new ExitGames.Client.Photon.Hashtable();   
                hashtable_player["currentMoney"] = carMovement.playerData.currentMoney;
                hashtable_player["debt"] = carMovement.playerData.debt;
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable_player);
                
                this.transform.GetChild(0).gameObject.SetActive(false);
                carMovement.TurnEnd();  //TurnEnd
            }
        }
    }
}
