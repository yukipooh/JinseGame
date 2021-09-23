using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
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
                }
                break;
            case EnumDefinitions.TileType.BIRTH:
                playerData.familyNum++; //子供追加
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
