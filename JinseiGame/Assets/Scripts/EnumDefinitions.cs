using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefinitions
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
        BRANCH, //枝分かれ
    }

    public enum Job {
        FREETER,    //フリーター
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

    public enum House{
        NONE,
        MANSION,    //マンション
        APART,  //アパート
        NORMAL, //一軒家
        CASTLE, //お城
        GOUTEI, //豪邸
        
    }

    public enum Course {
        START = 0,
        BUSINESS = 1,
        SPECIAL = 2,
        MAIN_FIRST = 3,
        HELL = 4,
        HEAVEN = 5,
        MAIN_SECOND = 6,
        GAMBLE = 7,
        WORLD = 8,
        MAIN_THIRD = 9,
    }
}
