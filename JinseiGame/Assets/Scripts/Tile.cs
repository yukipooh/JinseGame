using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileInfo tileInfo;
    
    public void Stopped(){
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
