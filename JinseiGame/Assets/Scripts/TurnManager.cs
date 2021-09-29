using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class TurnManager : MonoBehaviourPunCallbacks
{
    List<Player> _playerTurnOrder;
    List<Player> _players = new List<Player>();

    [SerializeField] UIManager uIManager;

    public int currentTurnIndex = 0;  //現在のターンのプレイヤーのindex (_playerTurnOrderの)
    public static bool isFirstTurn = true; //プレイヤーにとって一番最初のターンかどうかのフラグ

    public void Initialize(List<Player> players){
        _players = players;

        _playerTurnOrder = _players;
        

        for(int i = 0; i < _players.Count; i++){
            SetPlayerTurnOrder(i, _players[i]); //仮
        }

        FirstTurn();
    }

    public void SetPlayerTurnOrder(int index, Player player){
        _playerTurnOrder[index] = player;
    }

    public void FirstTurn(){
        var hashtable_room = new ExitGames.Client.Photon.Hashtable();
        hashtable_room["turn"] = GetCurrentTurnPlayer().NickName;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable_room);

        //自分のターンだったら
        if(PhotonNetwork.LocalPlayer == _playerTurnOrder[currentTurnIndex]){
            uIManager.ShowAllDefaultUI();
            uIManager.ShowCourseSelectPanel();
            isFirstTurn = false;
        }else{
            uIManager.DismissAllDefaultUI(true,true,true,false);
            uIManager.DismissCourseSelectPanel();
        }

        
    }

    public void MoveToNextTurn(){
        currentTurnIndex++;
        if(currentTurnIndex >= _players.Count){
            currentTurnIndex = 0;
        }

        var hashtable_room = new ExitGames.Client.Photon.Hashtable();
        hashtable_room["turn"] = GetCurrentTurnPlayer().NickName;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable_room);

        //自分のターンだったら
        if(PhotonNetwork.LocalPlayer == _playerTurnOrder[currentTurnIndex]){
            uIManager.ShowAllDefaultUI();
            if(isFirstTurn){
                uIManager.ShowCourseSelectPanel();
                isFirstTurn = false;
            }
        }else{
            uIManager.DismissAllDefaultUI(true,true,true,false);
        }
    }

    public Player GetCurrentTurnPlayer(){
        return _playerTurnOrder[currentTurnIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
