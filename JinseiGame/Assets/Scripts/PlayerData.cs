using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int currentMoney = 10000;   //所持金
    public int debt = 0;   //借金
    public EnumDefinitions.Job job = EnumDefinitions.Job.FREETER; //職業
    public EnumDefinitions.House house = EnumDefinitions.House.NONE;    //持ち家
    public int familyNum = 1;  //家族人数
    public List<EnumDefinitions.Treasure> treasures;   //持ってるお宝
    public List<EnumDefinitions.Insurance> insurances; //入ってる保険
    public bool isGoaled = false;   //ゴール済みかどうかのフラグ
}
