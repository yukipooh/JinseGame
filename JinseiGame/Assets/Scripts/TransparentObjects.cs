using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjects : MonoBehaviour
{
    List<CarMovement> carMovements = GameManager.carMovements;
    RaycastHit hit;
    GameObject lastHitObject;   //最後にhitしたオブジェクトを格納
    public LayerMask layerMask; //これで指定したレイヤーにのみrayがあたる

    [SerializeField] Material transparentMaterial;  //透明マテリアル
    List<Material> storedMaterial = new List<Material>();    //マテリアルを切り替えたときの切り替え元を補完しておく


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = this.transform.position - carMovements[0].transform.position;
        if(Physics.Raycast(carMovements[0].transform.position, direction, out hit, direction.magnitude, layerMask)){
            Debug.Log("hit");
            if(storedMaterial.Count == 0){
                MeshRenderer[] allMeshRenderer = hit.transform.GetComponentsInChildren<MeshRenderer>();
                for(int i = 0; i < hit.transform.childCount; i++){
                    storedMaterial.Add(allMeshRenderer[i].material);
                    Debug.Log("transparent");
                    allMeshRenderer[i].material = transparentMaterial;  //透明をセット
                    lastHitObject = hit.transform.gameObject;

                }
            }
        }else{
            if(lastHitObject){
                MeshRenderer[] allMeshRenderer = lastHitObject.transform.GetComponentsInChildren<MeshRenderer>();
                for(int i = 0; i < lastHitObject.transform.childCount; i++){
                    allMeshRenderer[i].material = storedMaterial[i];
                }
                lastHitObject = null;
                storedMaterial.Clear(); //削除
            }
        }
    }
}
