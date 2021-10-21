using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerContent : MonoBehaviour
{
    [SerializeField] Text _playerNameText; 
    [SerializeField] Toggle _toggle; //CheckBox
    
    public Player _player;
    public bool _isReady => _toggle.isOn;

    public void SetPlayerInfo(Player player){
        _player = player;
        _playerNameText.text = player.NickName;
    }
}
