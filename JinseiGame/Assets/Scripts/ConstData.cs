using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstData : MonoBehaviour
{
    [Header("コースのオブジェクト")]
    [SerializeField] GameObject start;
    [SerializeField] GameObject businessCourse;
    [SerializeField] GameObject specialCourse;
    [SerializeField] GameObject mainFirst;
    [SerializeField] GameObject hellCourse;
    [SerializeField] GameObject heavenCourse;
    [SerializeField] GameObject mainSecond;
    [SerializeField] GameObject gambleCourse;
    [SerializeField] GameObject worldCourse;
    [SerializeField] GameObject mainThird;
    // [Header("----------------------")]

    public static string[] jobName = {
        "フリーター",
        "コック",
        "デザイナー",
        "アスリート",
        "エンジニア",
        "弁護士",
        "医者",
        "ビジネスマン",
        "ギャンブラー",
        "石油取引人",
        "コック長",
        "トップデザイナー",
        "トップアスリート",
        "フルスタックエンジニア",
        "カリスマ弁護士",
        "病院長",
        "部長",
        "凄腕ギャンブラー",
        "石油王",
    };

    public static Dictionary<EnumDefinitions.Course, GameObject> Courses = new Dictionary<EnumDefinitions.Course, GameObject>();
    public static Dictionary<int, int> CourseLink = new Dictionary<int, int>(); //<コース前、コース後>
    public static Dictionary<EnumDefinitions.Job, int> Salaries = new Dictionary<EnumDefinitions.Job, int>();   //職業の給料
    public static Dictionary<EnumDefinitions.Treasure,int> TreasureValues = new Dictionary<EnumDefinitions.Treasure, int>(); // お宝の価値

    public void Initialize(){
//Dictionaryにデータの追加
        Courses.Add(EnumDefinitions.Course.START, start);
        Courses.Add(EnumDefinitions.Course.BUSINESS, businessCourse);
        Courses.Add(EnumDefinitions.Course.SPECIAL, specialCourse);
        Courses.Add(EnumDefinitions.Course.MAIN_FIRST, mainFirst);
        Courses.Add(EnumDefinitions.Course.HELL, hellCourse);
        Courses.Add(EnumDefinitions.Course.HEAVEN, heavenCourse);
        Courses.Add(EnumDefinitions.Course.MAIN_SECOND, mainSecond);
        Courses.Add(EnumDefinitions.Course.GAMBLE, gambleCourse);
        Courses.Add(EnumDefinitions.Course.WORLD, worldCourse);
        Courses.Add(EnumDefinitions.Course.MAIN_THIRD, mainThird);

        CourseLink.Add((int)EnumDefinitions.Course.START, (int)EnumDefinitions.Course.BUSINESS);
        CourseLink.Add((int)EnumDefinitions.Course.BUSINESS, (int)EnumDefinitions.Course.MAIN_FIRST);
        CourseLink.Add((int)EnumDefinitions.Course.SPECIAL, (int)EnumDefinitions.Course.MAIN_FIRST);
        CourseLink.Add((int)EnumDefinitions.Course.MAIN_FIRST, (int)EnumDefinitions.Course.HELL);
        CourseLink.Add((int)EnumDefinitions.Course.HELL, (int)EnumDefinitions.Course.MAIN_SECOND);
        CourseLink.Add((int)EnumDefinitions.Course.HEAVEN, (int)EnumDefinitions.Course.MAIN_SECOND);
        CourseLink.Add((int)EnumDefinitions.Course.MAIN_SECOND, (int)EnumDefinitions.Course.GAMBLE);
        CourseLink.Add((int)EnumDefinitions.Course.GAMBLE, (int)EnumDefinitions.Course.MAIN_THIRD);
        CourseLink.Add((int)EnumDefinitions.Course.WORLD, (int)EnumDefinitions.Course.MAIN_THIRD);
    
        Salaries.Add(EnumDefinitions.Job.FREETER, 12000);
        Salaries.Add(EnumDefinitions.Job.CHEF, 35000);
        Salaries.Add(EnumDefinitions.Job.DESIGNER, 40000);
        Salaries.Add(EnumDefinitions.Job.ATHLETE, 45000);
        Salaries.Add(EnumDefinitions.Job.ENGINEER, 42500);
        Salaries.Add(EnumDefinitions.Job.LAWYER, 50000);
        Salaries.Add(EnumDefinitions.Job.DOCTER, 55000);
        Salaries.Add(EnumDefinitions.Job.SALARYMAN, 20000);
        Salaries.Add(EnumDefinitions.Job.GAMBLER, 7500);
        Salaries.Add(EnumDefinitions.Job.OIL_TRANSPORTER, 35000);
        //ランクアップ↓----------------------------------------------
        Salaries.Add(EnumDefinitions.Job.CHEF_MASTER, 65000);
        Salaries.Add(EnumDefinitions.Job.TOP_DESIGNER, 70000);
        Salaries.Add(EnumDefinitions.Job.TOP_ATHLETE, 80000);
        Salaries.Add(EnumDefinitions.Job.FULL_STACK_ENGINEER, 70000);
        Salaries.Add(EnumDefinitions.Job.TOP_LAWYER, 82500);
        Salaries.Add(EnumDefinitions.Job.DOCTER_MASTER, 85000);
        Salaries.Add(EnumDefinitions.Job.SALARYMAN_MASTER, 50000);
        Salaries.Add(EnumDefinitions.Job.TOP_GAMBLER, 15000);
        Salaries.Add(EnumDefinitions.Job.OIL_MASTER, 100000);

        TreasureValues.Add(EnumDefinitions.Treasure.TELESCOPE,40000);
        TreasureValues.Add(EnumDefinitions.Treasure.ROBOT,48000);
        TreasureValues.Add(EnumDefinitions.Treasure.FOSSIL,95000);
        TreasureValues.Add(EnumDefinitions.Treasure.DIAMOND,120000);
        TreasureValues.Add(EnumDefinitions.Treasure.FIGURE,60000);
        TreasureValues.Add(EnumDefinitions.Treasure.WATCH,80000);
        TreasureValues.Add(EnumDefinitions.Treasure.PAINTING,85000);
        TreasureValues.Add(EnumDefinitions.Treasure.RING,72000);
        TreasureValues.Add(EnumDefinitions.Treasure.GAME_MACHINE,50000);
        TreasureValues.Add(EnumDefinitions.Treasure.PIANO,90000);
        TreasureValues.Add(EnumDefinitions.Treasure.TABLET,45000);
        TreasureValues.Add(EnumDefinitions.Treasure.SMARTPHONE,64000);
        TreasureValues.Add(EnumDefinitions.Treasure.PROJECTOR,50000);
    }
}
