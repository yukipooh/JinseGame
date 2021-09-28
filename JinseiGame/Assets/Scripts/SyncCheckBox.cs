using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SyncCheckBox : MonoBehaviourPunCallbacks
{
    [SerializeField] Toggle toggle;
    bool isChanged = false;

    void Start() {
        toggle.onValueChanged.AddListener(value => OnToggleChanged());    
    }

    [PunRPC]
    void Sync(){
        //抜けるとバグる
        Debug.Log("hello");
        bool tmp = !toggle.isOn;
        isChanged = true;
        toggle.isOn = tmp;
    }

    void OnToggleChanged(){
        if(!isChanged){
            photonView.RPC("Sync",RpcTarget.OthersBuffered);
        }
        isChanged = false;
    }

    public void SetInteractable(){
        if(transform.parent.parent.GetComponent<PlayerContent>()._player == PhotonNetwork.LocalPlayer){
            toggle.interactable = true;
        }
    }
}
