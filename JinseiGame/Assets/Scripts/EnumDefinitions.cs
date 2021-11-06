using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefinitions
{
    public enum TileType {
        START,  //スタート
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
        CHEF,   //コック
        DESIGNER,   //デザイナー
        ATHLETE,    //アスリート
        ENGINEER,   //エンジニア
        LAWYER, //弁護士
        DOCTER, //医者
        SALARYMAN,  //ビジネスマン
        GAMBLER,    //ギャンブラー
        OIL_TRANSPORTER,    //石油取引人
        //ランクアップ↓---------------------------
        CHEF_MASTER,    //コック長
        TOP_DESIGNER,   //トップデザイナー
        TOP_ATHLETE,    //トップアスリート
        FULL_STACK_ENGINEER,    //フルスタックエンジニア
        TOP_LAWYER, //カリスマ弁護士
        DOCTER_MASTER,  //病院長
        SALARYMAN_MASTER,   //部長
        TOP_GAMBLER,    //凄腕ギャンブラー
        OIL_MASTER, //石油王
        
    }

    public enum Treasure{
        TELESCOPE,
        ROBOT,
        FOSSIL,
        DIAMOND,
        FIGURE,
        WATCH,
        PAINTING,
        RING,
        GAME_MACHINE,
        PIANO,
        TABLET,
        SMARTPHONE,
        PROJECTOR,
        MAIL,   //お祈りメール
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
        GOUTEI, //豪邸
        CASTLE, //お城
        
    }

    public enum HouseStructure{
        TEKKIN, //鉄筋コンクリート
        TEKKOTU,    //鉄骨
        MOKUZO, //木造
        

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
