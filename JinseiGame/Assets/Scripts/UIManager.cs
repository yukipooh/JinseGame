using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Text playerNameText;
    [SerializeField] Text currentMoneyText;
    [SerializeField] Text debtText;
    [SerializeField] Text descriptionText;
    [SerializeField] Text jobText;
    [SerializeField] Text turnText;
    [SerializeField] GameObject rouletteForMove;
    [SerializeField] GameObject rouletteForDecision;
    [SerializeField] GameObject courseSelectPanel;

    public void Initialize(){

    }

    public void DismissAllDefaultUI(bool name = false, bool money = false, bool description = false, bool roulette = false){
        //通常の画面で表示されているUIを全て非表示に
        playerNameText.gameObject.SetActive(name);
        currentMoneyText.gameObject.SetActive(money);
        descriptionText.gameObject.SetActive(description);
        rouletteForMove.SetActive(roulette);
    }

    public void ShowAllDefaultUI(bool name = true, bool money = true, bool description = true, bool roulette = true){
        //通常の画面で表示されているUIを全て表示
        playerNameText.gameObject.SetActive(name);
        currentMoneyText.gameObject.SetActive(money);
        descriptionText.gameObject.SetActive(description);
        rouletteForMove.SetActive(roulette);
    }

    public void DismissCourseSelectPanel(){
        courseSelectPanel.SetActive(false);
    }
    public void ShowCourseSelectPanel(){
        courseSelectPanel.SetActive(true);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer != PhotonNetwork.LocalPlayer) return;
        foreach(var prop in changedProps){
            switch(prop.Key){
                case "currentMoney":
                    currentMoneyText.text = "所持金：" + prop.Value.ToString() + "$";
                    break;
                case "debt":
                    debtText.text = "借金：" + prop.Value.ToString() + "$";
                    break;
                case "job":
                    jobText.text = "職業：" + prop.Value;
                    break;
                case "familyNum":

                    break;
                default:
                    break;
            }
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        foreach(var prop in propertiesThatChanged){
            switch(prop.Key){
                case "description":
                    descriptionText.text = prop.Value.ToString();
                    break;
                case "turn":
                    turnText.text = prop.Value.ToString() + "のターン！";
                    break;
                default:
                    break;
            }
        }
    }
}
