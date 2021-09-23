using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject startTile;
    

    public static GameObject localCar;

    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        Debug.Log("マスターサーバーへ接続");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        Debug.Log("ゲームサーバーへ接続");
        PhotonNetwork.NickName = TitleUI.playerName;
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        Vector3 startPos = startTile.transform.position;
        PhotonNetwork.Instantiate("Car", startPos + new Vector3(PhotonNetwork.CountOfPlayersInRooms,0,0), Quaternion.identity);
        
    }
}