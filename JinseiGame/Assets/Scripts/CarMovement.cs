using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] GameObject[] tilePoints;    //マス
    NavMeshAgent navMeshAgent;
    Tween tween;
    Vector3 latestPosition;
    int currentNum = 0; //現在いるタイル
    Rigidbody rigidbody;
    const float INTERVAL = 0.5f;    //次のマスに進むまでに待つ時間
    

    const float ROTATE_FORWARD_ADJUST_ANGLE = 90;

    void Start() {
        latestPosition = transform.position;    
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        
    }

    public void MoveForward(int tileNum){
        if(tileNum < tilePoints.Length){
            //this.tween = transform.DOMove(tilePoints[tileNum].transform.position,1);
            navMeshAgent.SetDestination(tilePoints[tileNum].transform.position);
            this.currentNum++;
        }
    }

    
    IEnumerator Dice(){
        int dice = int.Parse(RouletteController.result);   //ルーレット回すよ
        Debug.Log(dice);
        for(int i = 0; i < dice; i++){
            MoveForward(currentNum + 1);
            yield return new WaitForSeconds(INTERVAL);
        }
        //移動し終わった
        tilePoints[currentNum].GetComponent<Tile>().Stopped();
    }

    void AdjustAngle(){
        RaycastHit hit;
        if(Physics.Raycast(
            transform.position,
            -transform.up,
            out hit,
            float.PositiveInfinity
            )){
                // 傾きの差を求める
                Quaternion q = Quaternion.FromToRotation(
                    transform.up,
                    hit.normal);

                // 自分を回転させる
                transform.rotation *= q;
            }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.W)){
            StartCoroutine("Dice");
        }    
        
    }

    void LookForward(){
        Vector3 diff = transform.position - latestPosition;
        latestPosition = transform.position;

        if(diff.magnitude > 0.1f){
            transform.rotation = Quaternion.LookRotation(diff);
            transform.Rotate(new Vector3(0,ROTATE_FORWARD_ADJUST_ANGLE,0));
        }
    }
}
