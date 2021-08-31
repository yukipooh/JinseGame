using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 100;
    public float zoomSpeed = 400;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.W)){
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        
        float scroll = Input.mouseScrollDelta.y;
        transform.Translate(Vector3.forward * scroll * Time.deltaTime * zoomSpeed); //ズーム
    }
}
