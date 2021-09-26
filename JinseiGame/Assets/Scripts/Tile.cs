using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Tile : MonoBehaviourPunCallbacks
{
    public TileInfo tileInfo;
    CourseSelect courseSelect;
    [SerializeField] Text descriptionText;
    
    void Start() {
        courseSelect = GameObject.Find("CourseSelect").GetComponent<CourseSelect>();
    }
    
    public void Stopped(ref PlayerData playerData){
        switch(tileInfo.tileType){
            case EnumDefinitions.TileType.START:
                
                break;
            case EnumDefinitions.TileType.MONEY:
                if((playerData.currentMoney + tileInfo.money_delta) < 0){
                    playerData.debt += -1 * (playerData.currentMoney + tileInfo.money_delta);
                    playerData.currentMoney = 0;
                    break;
                }
                playerData.currentMoney += tileInfo.money_delta;
                break;
            case EnumDefinitions.TileType.EMPLOY:
                playerData.job = tileInfo.job;
                CarMovement carMovement = playerData.gameObject.GetComponent<CarMovement>();
                if(tileInfo.isMoveToNextCourseTile){
                    StartCoroutine(MoveToNextCourseTile(carMovement));
                }
                break;
            case EnumDefinitions.TileType.JOB_RANKUP:
                break;
            case EnumDefinitions.TileType.MARRY:
                if(playerData.familyNum == 1){
                    playerData.familyNum++; //配偶者を追加
                    GameObject pin = PhotonNetwork.Instantiate("FemalePin",new Vector3(0,0,0),Quaternion.identity);
                    pin.transform.parent = playerData.transform.GetChild(0).GetChild(0);
                    pin.transform.localPosition = new Vector3(-0.06518836f,0.00303f,0.01046f);
                    pin.transform.localRotation = Quaternion.Euler(0,0,-135);

                }
                break;
            case EnumDefinitions.TileType.BIRTH:
                playerData.familyNum++; //子供追加
                switch(playerData.familyNum){
                    case 3:
                        GameObject child_1 = PhotonNetwork.Instantiate("ChildPin",new Vector3(0,0,0),Quaternion.identity);
                        child_1.transform.parent = playerData.transform.GetChild(0).GetChild(2);
                        child_1.transform.localPosition = new Vector3(-0.06518836f,0.00303f,0.00934f);
                        child_1.transform.localRotation = Quaternion.Euler(0,0,-135);
                        break;
                    case 4:
                        GameObject child_2 = PhotonNetwork.Instantiate("ChildPin",new Vector3(0,0,0),Quaternion.identity);
                        child_2.transform.parent = playerData.transform.GetChild(0).GetChild(3);
                        child_2.transform.localPosition = new Vector3(-0.06518836f,0.00303f,0.00934f);
                        child_2.transform.localRotation = Quaternion.Euler(0,0,-135);
                        break;
                    case 5:
                        GameObject child_3 = PhotonNetwork.Instantiate("ChildPin",new Vector3(0,0,0),Quaternion.identity);
                        child_3.transform.parent = playerData.transform.GetChild(0).GetChild(4);
                        child_3.transform.localPosition = new Vector3(-0.06518836f,0.00303f,0.00934f);
                        child_3.transform.localRotation = Quaternion.Euler(0,0,-135);
                        break;
                    case 6:
                        GameObject child_4 = PhotonNetwork.Instantiate("ChildPin",new Vector3(0,0,0),Quaternion.identity);
                        child_4.transform.parent = playerData.transform.GetChild(0).GetChild(5);
                        child_4.transform.localPosition = new Vector3(-0.06518836f,0.00303f,0.00934f);
                        child_4.transform.localRotation = Quaternion.Euler(0,0,-135);
                        break;
                }
                break;
            case EnumDefinitions.TileType.TREASURE:
                playerData.treasures.Add(tileInfo.treasure);
                break;
            case EnumDefinitions.TileType.HOUSING:
                break;
            case EnumDefinitions.TileType.INSURANCE:
                playerData.insurances.Add(tileInfo.insurance);  //保険追加
                break;
            case EnumDefinitions.TileType.SETTLE:
                break;
            case EnumDefinitions.TileType.GOAL:
                break;
            case EnumDefinitions.TileType.BRANCH:
                if(gameObject.name == "0031_branch"){
                    //天国コースと地獄コースへの分岐マス
                    courseSelect.ShowPanel(1);
                }
                if(gameObject.name == "0073_branchB"){
                    courseSelect.ShowPanel(2);
                }
                
                break;
        }

        descriptionText.transform.parent.gameObject.SetActive(true);
        descriptionText.text = tileInfo.description;

        if(tileInfo.isSalaryTile){
            playerData.currentMoney += ConstData.Salaries[playerData.job];  //給料追加
            descriptionText.text += (ConstData.Salaries[playerData.job].ToString() + "$だ！");
        }
        
    }

    //このコルーチン　isRedのマスに適用するとDiceが重複してバグるから注意
    IEnumerator MoveToNextCourseTile(CarMovement carMovement){
        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }
        carMovement.StartCoroutine(carMovement.Dice(false,transform.parent.childCount - transform.GetSiblingIndex()));
    }

}
