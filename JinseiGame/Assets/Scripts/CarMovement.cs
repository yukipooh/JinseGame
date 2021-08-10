using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] GameObject[] tilePoints;    //マス
    Tween tween;
    Vector3 latestPosition;
    int currentNum = 0;

    const float ROTATE_FORWARD_ADJUST_ANGLE = 90;

    void Start() {
        latestPosition = transform.position;    
    }

    public void MoveForward(int tileNum){
        if(tileNum < tilePoints.Length){
            this.tween = transform.DOMove(tilePoints[tileNum].transform.position,3);
            this.currentNum++;
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.W)){
            MoveForward(currentNum + 1);
        }    
        LookForward();
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
