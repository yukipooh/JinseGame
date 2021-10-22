using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject startTile;
    

    public static GameObject localCar;

    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        // PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.IsMessageQueueRunning = true;
        
    }

    public void Initialize(){
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        Vector3 startPos = startTile.transform.position;
        GameObject carObject = PhotonNetwork.Instantiate("Car", startPos + new Vector3(PhotonNetwork.CountOfPlayersInRooms,0,0), Quaternion.identity);
        carObject.transform.GetChild(0).GetChild(12).GetComponent<MeshRenderer>().material.color = GetCarColor(PhotonNetwork.LocalPlayer);
        StartCoroutine(nameof(ChangeAllCarColor));
        
        Debug.Log(PhotonNetwork.NickName);
    }

    //プレイヤーに保存されている車のColorを取得する
    public static Color GetCarColor(Player player){
        Color color = new Color(
            (float)player.CustomProperties["carColor_R"],
            (float)player.CustomProperties["carColor_G"],
            (float)player.CustomProperties["carColor_B"],
            (float)player.CustomProperties["carColor_A"]
        );
        return color;
    }

    IEnumerator ChangeAllCarColor(){
        //全員分のcarMovementが揃うまで処理を待つ
        while(GameManager.carMovements.Count < PhotonNetwork.PlayerList.Length){
            yield return null;
        }
        foreach(CarMovement carMovement in GameManager.carMovements){
            //色同期
            carMovement.gameObject.transform.GetChild(0).GetChild(12).GetComponent<MeshRenderer>().material.color
             = GetCarColor(carMovement.photonView.Owner);
        }
    }

    // // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    // public override void OnConnectedToMaster() {
    //     // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
    //     // Debug.Log("マスターサーバーへ接続");
    //     // PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    // }

    // // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    // public override void OnJoinedRoom() {
    //     // Debug.Log("ゲームサーバーへ接続");
    //     // PhotonNetwork.NickName = TitleUI.playerName;
        
    // }
}