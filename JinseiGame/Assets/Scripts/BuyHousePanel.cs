using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BuyHousePanel : MonoBehaviourPunCallbacks
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button buyButton;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] Image houseImage;
    [SerializeField] Text houseName;
    [SerializeField] Text housePrice;
    [SerializeField] Text houseArea;
    [SerializeField] Text houseAddress;
    [SerializeField] Text houseAge;
    [SerializeField] Text houseStructure;
    [SerializeField] Text houseCapacity;
    [SerializeField] Text houseStory;
    [SerializeField] GameObject buyPanel;
    [SerializeField] GameObject buyHousePanel;  
    [SerializeField] TurnManager turnManager;
    EnumDefinitions.House currentPage = EnumDefinitions.House.MANSION;
    
    List<HouseData> houseDatas = new List<HouseData>();
    [SerializeField] Sprite[] houseSprites;
    string[] houseStructureStrings = {"鉄筋コンクリート","鉄骨","木造"};
    
    

    void Start() {
        houseDatas.Add(new HouseData(
            "人生マンション",    //名前
            250000, //価格
            "住所", //住所
            15, //築年数
            EnumDefinitions.HouseStructure.TEKKOTU,  //構造
            20, //入居可能人数
            3,   //階建
            houseSprites[0],
            35 //面積
        ));
        houseDatas.Add(new HouseData(
            "人生アパートよこい",    //名前
            150000, //価格
            "住所", //住所
            25, //築年数
            EnumDefinitions.HouseStructure.MOKUZO,  //構造
            5, //入居可能人数
            2,   //階建
            houseSprites[1],
            20 //面積
        ));
        houseDatas.Add(new HouseData(
            "マイホーム",    //名前
            400000, //価格
            "住所", //住所
            6, //築年数
            EnumDefinitions.HouseStructure.TEKKOTU,  //構造
            1, //入居可能人数
            1,   //階建
            houseSprites[2],
            53 //面積
        ));
        houseDatas.Add(new HouseData(
            "海の見える豪邸",    //名前
            600000, //価格
            "住所", //住所
            5, //築年数
            EnumDefinitions.HouseStructure.TEKKIN,  //構造
            1, //入居可能人数
            3,   //階建
            houseSprites[3],
            60 //面積
        ));
        houseDatas.Add(new HouseData(
            "人生城",    //名前
            1000000, //価格
            "住所", //住所
            150, //築年数
            EnumDefinitions.HouseStructure.TEKKIN,  //構造
            1, //入居可能人数
            3,   //階建
            houseSprites[4],
            100 //面積
        ));

        leftButton.onClick.AddListener(() => OnClickLeftButton());
        rightButton.onClick.AddListener(() => OnClickRightButton());
        buyButton.onClick.AddListener(() => OnClickBuyButton());
        yesButton.onClick.AddListener(() => OnClickYesButton());
        noButton.onClick.AddListener(() => OnClickNoButton());
    }

    void OnClickLeftButton(){
        if((int)currentPage <= 1) return;    //ページの範囲指定
        currentPage--;  //ページ移動
        houseImage.sprite = houseDatas[(int)currentPage - 1].sprite;
        houseName.text = houseDatas[(int)currentPage - 1].name;
        housePrice.text = "価格： $" + houseDatas[(int)currentPage - 1].price;
        houseArea.text = "間取り専有面積： " + houseDatas[(int)currentPage - 1].area + "㎡";
        houseAddress.text = "住所： " + houseDatas[(int)currentPage - 1].address;
        houseAge.text = "築年数： " + houseDatas[(int)currentPage - 1].age + "年";
        houseStructure.text = "構造： " + houseStructureStrings[(int)houseDatas[(int)currentPage - 1].structure];
        houseCapacity.text = "総戸数： " + houseDatas[(int)currentPage - 1].capacity + "人入居可能";
        houseStory.text = "階建： " + houseDatas[(int)currentPage - 1].story + "階建";
    }

    void OnClickRightButton(){
        if((int)currentPage >= 5) return;    //ページの範囲指定
        currentPage++;  //ページ移動
        houseImage.sprite = houseDatas[(int)currentPage - 1].sprite;
        houseName.text = houseDatas[(int)currentPage - 1].name;
        housePrice.text = "価格： $" + houseDatas[(int)currentPage - 1].price;
        houseArea.text = "間取り専有面積： " + houseDatas[(int)currentPage - 1].area + "㎡";
        houseAddress.text = "住所： " + houseDatas[(int)currentPage - 1].address;
        houseAge.text = "築年数： " + houseDatas[(int)currentPage - 1].age + "年";
        houseStructure.text = "構造： " + houseStructureStrings[(int)houseDatas[(int)currentPage - 1].structure];
        houseCapacity.text = "総戸数： " + houseDatas[(int)currentPage - 1].capacity + "人入居可能";
        houseStory.text = "階建： " + houseDatas[(int)currentPage - 1].story + "階建";
    }

    void OnClickBuyButton(){
        buyPanel.SetActive(true);
    }

    void OnClickYesButton(){
        buyPanel.SetActive(false);
        buyHousePanel.SetActive(false);
        if(turnManager.GetCurrentTurnPlayer() == PhotonNetwork.LocalPlayer){
            Buy();
            foreach(CarMovement carMovement in GameManager.carMovements){
                if(carMovement.photonView.IsMine){
                    carMovement.TurnEnd();  //ターンエンド
                }
            }
        }
    }

    void OnClickNoButton(){
        buyPanel.SetActive(false);
    }

    void Buy(){
        foreach(CarMovement carMovement in GameManager.carMovements){
            if(carMovement.photonView.IsMine){
                carMovement.playerData.house = currentPage;
                if((carMovement.playerData.currentMoney - houseDatas[(int)currentPage - 1].price) < 0){
                    carMovement.playerData.debt += -1 * (carMovement.playerData.currentMoney - houseDatas[(int)currentPage - 1].price);
                    carMovement.playerData.currentMoney = 0;
                }else{
                    carMovement.playerData.currentMoney -= houseDatas[(int)currentPage - 1].price;

                }
                Debug.Log($"{carMovement.playerData.house}を購入しました");

                var hashtable_player = new ExitGames.Client.Photon.Hashtable();   
                hashtable_player["currentMoney"] = carMovement.playerData.currentMoney;
                hashtable_player["debt"] = carMovement.playerData.debt;
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable_player);
            }
        }
    }
}
