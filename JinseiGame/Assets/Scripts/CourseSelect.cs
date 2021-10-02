using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CourseSelect : MonoBehaviour
{
    [SerializeField] GameObject courseSelectPanel;
    [SerializeField] GameObject decideRoulette;
    [SerializeField] Text rightCourseButtonText;
    [SerializeField] Text leftCourseButtonText;
    [SerializeField] Button rightCourseButton;
    [SerializeField] Button leftCourseButton;
    // [SerializeField] GameObject rightCourse;
    // [SerializeField] GameObject leftCourse;

    string[] leftButtonCourseNames = {"専門職コース","地獄コース","ギャンブルコース"};
    EnumDefinitions.Course[] leftButtonCourses = {EnumDefinitions.Course.SPECIAL,EnumDefinitions.Course.HELL,EnumDefinitions.Course.GAMBLE};
    string[] rightButtonCourseNames = {"ビジネスコース","天国コース","世界旅行コース"};
    EnumDefinitions.Course[] rightButtonCourses = {EnumDefinitions.Course.BUSINESS,EnumDefinitions.Course.HEAVEN,EnumDefinitions.Course.WORLD};
    EnumDefinitions.Course[] courseLinkOriginCourses = {EnumDefinitions.Course.START,EnumDefinitions.Course.MAIN_FIRST,EnumDefinitions.Course.MAIN_SECOND};
    void Start() {
        ShowPanel(0);
        courseSelectPanel.SetActive(false);
    }
    /// <param name="index">何番目の分岐か(0番目 = ビジネスor専門)</param>
    public void ShowPanel(int index){
        courseSelectPanel.SetActive(true);
        rightCourseButtonText.text = rightButtonCourseNames[index];
        leftCourseButtonText.text = leftButtonCourseNames[index];
        rightCourseButton.onClick.AddListener(() => OnClickRightCourseButton(index));
        leftCourseButton.onClick.AddListener(() => OnClickLeftCourseButton(index));
    }

    public void ShowRoulette(){
        decideRoulette.transform.parent.parent.gameObject.SetActive(true);
        StartCoroutine(nameof(WaitRouletteStop));
        
    }

    IEnumerator WaitRouletteStop(){
        RouletteController rouletteController = decideRoulette.GetComponent<RouletteController>();
        while(!rouletteController.isRouletteStopped){
            yield return null;
        }
        rouletteController.isRouletteStopped = false;
        OnRouletteFinallyStopped(rouletteController.GetResult());
        Debug.Log(rouletteController.GetResult().ToString());
    }

    void OnRouletteFinallyStopped(int result){
        if((result % 2) == 0){
            OnClickRightCourseButton(1);
        }else{
            OnClickLeftCourseButton(1);
        }
    }

    /// <param name="index">何番目の分岐か(0番目 = ビジネスor専門)</param>
    void OnClickRightCourseButton(int index){
        ConstData.CourseLink[(int)courseLinkOriginCourses[index]] = (int)rightButtonCourses[index];
        foreach(CarMovement carMovement in GameManager.carMovements){
            if(carMovement.photonView.IsMine){
                carMovement.isStopping = false;
            }
        }
        courseSelectPanel.SetActive(false);
        decideRoulette.transform.parent.parent.gameObject.SetActive(false);
    }
    void OnClickLeftCourseButton(int index){
        ConstData.CourseLink[(int)courseLinkOriginCourses[index]] = (int)leftButtonCourses[index];
        foreach(CarMovement carMovement in GameManager.carMovements){
            if(carMovement.photonView.IsMine){
                carMovement.isStopping = false;
            }
        }
        courseSelectPanel.SetActive(false);
        decideRoulette.transform.parent.parent.gameObject.SetActive(false);
    }
}
