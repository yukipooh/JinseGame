using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CarMovement : MonoBehaviour
{
    [SerializeField] GameObject roulette;   //RoulettePrefab
    [SerializeField] Text resultText; // resultText
    [SerializeField] Text descriptionText;  //description
    PlayerData playerData;
    NavMeshAgent navMeshAgent;
    Tween tween;
    Vector3 latestPosition;
    int currentNum = 0; //現在いるタイル
    public GameObject currentCourse;    //現在いるコース(オブジェクト)
    public EnumDefinitions.Course currentCourseEnum;  //現在いるコース(enum)
    Rigidbody rigidbody;
    const float INTERVAL = 0.5f;    //次のマスに進むまでに待つ時間

    public bool isStopping = false;    //車が一時停止しているかどうかのフラグ

    const float ROTATE_FORWARD_ADJUST_ANGLE = 90;

    public void Initialize(){
        latestPosition = transform.position;    
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();

        currentCourse = ConstData.Courses[EnumDefinitions.Course.START];
        currentCourseEnum = EnumDefinitions.Course.START;
    }

    public void MoveForward(int tileNum){
        descriptionText.transform.parent.gameObject.SetActive(false);
        if(tileNum < currentCourse.transform.childCount){
            navMeshAgent.SetDestination(currentCourse.transform.GetChild(tileNum).transform.position + new Vector3(0,2.8f,0));
            this.currentNum++;
        }
        if(tileNum == currentCourse.transform.childCount){
            Debug.Log("コース移動");
            //次のコースにうつろうとしているとき
            currentCourseEnum = (EnumDefinitions.Course)Enum.ToObject(
                typeof(EnumDefinitions.Course),ConstData.CourseLink[(int)currentCourseEnum]);     //コース移動
            currentCourse = ConstData.Courses[currentCourseEnum];   //オブジェクトも変更しとく
            currentNum = 0; //タイルのインデックスを初期化
            
            navMeshAgent.SetDestination(currentCourse.transform.GetChild(0).transform.position + new Vector3(0,2.8f,0));    //コースのスタートに移動

        }
    }
    
    public IEnumerator Dice(){
        int dice = int.Parse(RouletteController.result);   //ルーレット回すよ
        Debug.Log(dice);
        for(int i = 0; i < dice; i++){
            MoveForward(currentNum + 1);
            yield return new WaitForSeconds(INTERVAL);
            Tile currentTile = currentCourse.transform.GetChild(currentNum).GetComponent<Tile>();
            if(currentTile.tileInfo.isRed){
                currentTile.Stopped(ref playerData);    //通り過ぎたマスの効果を発揮
                isStopping = true;
                if(currentTile.tileInfo.tileType == EnumDefinitions.TileType.BRANCH){
                    while(isStopping){
                        yield return null; 
                    }
                }else{
                    while(!Input.GetKeyDown(KeyCode.Return)){
                        yield return null;
                    }
                }
            
            }
            if(currentTile.tileInfo.isMustStop){
                break;  //移動をやめる
            }
            resultText.text = (dice-i-1).ToString();
        }
        OnMoveEnd();    //移動し終わった
        roulette.SetActive(true);
    }

    void OnMoveEnd(){
        //移動し終わった
        currentCourse.transform.GetChild(currentNum).GetComponent<Tile>().Stopped(ref playerData);
        resultText.gameObject.SetActive(false);
        descriptionText.transform.parent.gameObject.SetActive(true);
    }

    void Update() {
        
    }
}
