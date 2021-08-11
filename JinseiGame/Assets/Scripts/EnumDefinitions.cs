using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefinitions : MonoBehaviour
{
    public enum TileType {
        START,  //スタート
        SALARY, //給料
        MONEY,  //お金変動
        EMPLOY, //就職
        JOB_RANKUP, //職業ランクアップ
        MARRY,  //結婚
        BIRTH,  //出産
        TREASURE,   //お宝
        HOUSING,    //家購入
        INSURANCE,  //保険契約
        SETTLE, //決算
        GOAL,   //ゴール
    }

    public enum Job {
        CHEF,
        DESIGNER,
        ATHLETE,
        ENGINEER,
        LAWYER,
        DOCTER,
        SALARYMAN,
        GAMBLER,
        YASUO_MAIN,
        POKEMON_MASTER,
    }

    public enum Treasure{
        TELESCOPE,
        ROBOT,
        KUWAGATA,
        FOSSIL,
    }

    public enum Insurance{
        KASAI,
        SEIMEI,
        JIDOSHA,
    }
}
