using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviourPunCallbacks
{
    const int MAX_PLAYER = 6;   //ルームの最大人数


    // Start is called before the first frame update
    void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;    //マスターに合わせてシーン遷移するように
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
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetContent(Player player, int index){
        GameObject playerContent = transform.GetChild(0).GetChild(index).gameObject;
        playerContent.transform.GetChild(1).gameObject.SetActive(false);    //NonPlayerPanelを非表示に
        playerContent.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);    //NonPlayerPanelを非表示に
        playerContent.GetComponent<PlayerContent>().SetPlayerInfo(player);
    }

    void ClearAllContent(){
        for(int i = 0; i < MAX_PLAYER; i++){
            GameObject playerContent = transform.GetChild(0).GetChild(i).gameObject;
            playerContent.transform.GetChild(1).gameObject.SetActive(true);    //NonPlayerPanelを非表示に
            playerContent.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);    //NonPlayerPanelを非表示に
        }
    }
}
