using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasurePanel : MonoBehaviour
{
    /// <summary>
    /// 宝物の画像リスト
    /// </summary>
    public List<Sprite> treasureSprites;
    GameObject gridObject;

    void Start() {
        gridObject = this.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// お宝リストにお宝を追加（表示）
    /// </summary>
    /// <param name="treasure">追加するお宝の種類</param>
    /// <param name="index">何番目に追加するかのインデックス</param>
    public void AddTreasure(EnumDefinitions.Treasure treasure, int index){
        gridObject.transform.GetChild(index).GetChild(0).GetComponent<Image>().sprite = treasureSprites[(int)treasure];
        gridObject.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
    }
}
