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

    public static Dictionary<EnumDefinitions.Course, GameObject> Courses = new Dictionary<EnumDefinitions.Course, GameObject>();
    public static Dictionary<int, int> CourseLink = new Dictionary<int, int>(); //<コース前、コース後>
    

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
    }
}
