using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChangeCarColor : MonoBehaviourPunCallbacks
{
    GameObject carBody;
    MeshRenderer meshRenderer;
    bool isFirst = true; //初期色を登録するためにフラグを用意
    [SerializeField] ColorPickerTriangle colorPickerTriangle;

    
    // Start is called before the first frame update
    void Start()
    {
        carBody = transform.GetChild(12).gameObject;
        meshRenderer = carBody.GetComponent<MeshRenderer>();
        colorPickerTriangle.SetNewColor(meshRenderer.material.color);
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.LocalPlayer == transform.parent.parent.GetComponent<PlayerContent>()._player){
            SetColor(colorPickerTriangle.TheColor);
        }
    }

    void SetColor(Color color){
        if(isFirst || (color != meshRenderer.material.color)){
            isFirst = false;
            var colorHash = new ExitGames.Client.Photon.Hashtable();
            colorHash["carColor_R"] = color.r;
            colorHash["carColor_G"] = color.g;
            colorHash["carColor_B"] = color.b;
            colorHash["carColor_A"] = color.a;
            PhotonNetwork.LocalPlayer.SetCustomProperties(colorHash);
        }
        meshRenderer.material.color = color;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer == transform.parent.parent.GetComponent<PlayerContent>()._player){
            Color color = new Color(
                (float)changedProps["carColor_R"],
                (float)changedProps["carColor_G"],
                (float)changedProps["carColor_B"],
                (float)changedProps["carColor_A"]
            );
            meshRenderer.material.color = color;
        }
    }
}
