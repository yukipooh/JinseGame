using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LobbyScene : MonoBehaviourPunCallbacks
{
    const int MAX_PLAYER = 6;   //ルームの最大人数
    Vector3[] colorPickerPositions = {
        new Vector3(-1,0.3f,-8.7f),
        new Vector3(),
        new Vector3(),
        new Vector3(),
        new Vector3(),
    };
    [SerializeField] Button startGameButton;
    [SerializeField] GameObject[] colorPickers;


    // Start is called before the first frame update
    void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;    //マスターに合わせてシーン遷移するように
    
        startGameButton.onClick.AddListener(() => MoveToGameScene());
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        Debug.Log("マスターサーバーへ接続");
        PhotonNetwork.NickName = TitleUI.playerName;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(){MaxPlayers = MAX_PLAYER}, TypedLobby.Default);

    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("ゲームサーバーへ接続");
        if(!PhotonNetwork.IsMasterClient){
            //マスタークライアントじゃない人はスタートボタンを非表示に
            startGameButton.gameObject.SetActive(false);
        }else{
            // int count = 0;
            // foreach(Player player in PhotonNetwork.PlayerList){
            //     if(transform.)
            // }
        }
        // PhotonNetwork.NickName = TitleUI.playerName;
        int count = 0;
        foreach(Player player in PhotonNetwork.PlayerList){
            SetContent(player,count);
            count++;
        }
    }

    //他のプレイヤーが入ってきたら
    public override void OnPlayerEnteredRoom(Player newPlayer){
        SetContent(newPlayer,PhotonNetwork.PlayerList.Length - 1);
    }

    public override void OnPlayerLeftRoom(Player leftPlayer){
        ClearAllContent();  //すべてのコンテントを初期化
        if(!PhotonNetwork.IsMasterClient){
            //マスタークライアントじゃない人はスタートボタンを非表示に
            startGameButton.gameObject.SetActive(false);
        }else{
            startGameButton.gameObject.SetActive(true);
        }
        int count = 0;
        foreach(Player player in PhotonNetwork.PlayerList){
            if(player != leftPlayer){
                //抜けた人以外のコンテントをセット
                Debug.Log("set");
                SetContent(player,count);
                count++;
            }
        }
    }
    
    public void MoveToGameScene(){
        PhotonNetwork.IsMessageQueueRunning = false;
        if(isAllReady()){
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["carColor_R"]);
            SceneManager.LoadScene("SampleScene");
        }else{
            Debug.Log("全員が準備完了である必要があります。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetContent(Player player, int index){
        GameObject playerContent = transform.GetChild(0).GetChild(index).gameObject;
        
        if(player.IsMasterClient){
            //スタートボタンを表示
            playerContent.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }else{
            //スタートボタンを非表示に
            playerContent.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
        playerContent.transform.GetChild(1).gameObject.SetActive(false);    //NonPlayerPanelを非表示に
        playerContent.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);    //NonPlayerPanelを非表示に
        if(player == PhotonNetwork.LocalPlayer){
            
            Debug.Log(colorPickers[index].transform.position);
            // colorPickers[index].GetComponent<PhotonView>().TransferOwnership(player);  //カラーピッカーのオーナーを設定
            colorPickers[index].SetActive(true);
        }
        playerContent.GetComponent<PlayerContent>().SetPlayerInfo(player);
        playerContent.transform.GetChild(0).GetChild(2).GetComponent<SyncCheckBox>().SetInteractable();
    }

    

    void ClearAllContent(){
        for(int i = 0; i < MAX_PLAYER; i++){
            GameObject playerContent = transform.GetChild(0).GetChild(i).gameObject;
            playerContent.transform.GetChild(1).gameObject.SetActive(true);    //NonPlayerPanelを非表示に
            playerContent.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);    //NonPlayerPanelを非表示に
            colorPickers[i].SetActive(false);

            playerContent.transform.GetChild(0).GetChild(2).GetComponent<Toggle>().isOn = false;    //全部チェック外す
            PhotonNetwork.RemoveBufferedRPCs(playerContent.transform.GetChild(0).GetChild(2).GetComponent<SyncCheckBox>().photonView.ViewID);
        }
    }

    bool isAllReady(){
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++){
            PlayerContent playerContent = transform.GetChild(0).GetChild(i).GetComponent<PlayerContent>();
            if(!playerContent._isReady) return false;
        }
        return true;
    }
}
