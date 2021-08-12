using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileInfo tileInfo;
    
    public void Stopped(ref PlayerData playerData){
        switch(tileInfo.tileType){
            case EnumDefinitions.TileType.START:
                
                break;
            case EnumDefinitions.TileType.SALARY:

                break;
            case EnumDefinitions.TileType.MONEY:
                playerData.currentMoney += tileInfo.money_delta;
                break;
            case EnumDefinitions.TileType.EMPLOY:
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
        }

        Debug.Log(tileInfo.tileType);
        Debug.Log(tileInfo.description);
        Debug.Log(tileInfo.money_delta);
        Debug.Log(tileInfo.job);
        Debug.Log(tileInfo.treasure);
        Debug.Log(tileInfo.insurance);
        Debug.Log(tileInfo.isRed);
        Debug.Log(tileInfo.isMustStop);
    }

}
