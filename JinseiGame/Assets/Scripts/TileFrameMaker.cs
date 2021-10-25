using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFrameMaker : MonoBehaviour
{
    MeshFilter meshFilter;
    Mesh mesh;
    [SerializeField] Material material;
    Material[] materials;
    [SerializeField] GameObject map;
    List<GameObject> tiles = new List<GameObject>();
    


    // Start is called before the first frame update
    void Start()
    {
        materials = map.GetComponent<WayMaterialChanger>().materials;
        int courseNum = map.transform.GetChild(0).childCount;   //コースの数
        for(int i = 0; i < courseNum; i++){
            for(int j = 0; j < map.transform.GetChild(0).GetChild(i).childCount; j++){
                tiles.Add(map.transform.GetChild(0).GetChild(i).GetChild(j).gameObject);
            }
        }
        

        foreach(GameObject tile in tiles){
            GameObject obj = new GameObject();
            obj.transform.parent = this.transform;
            obj.transform.localScale = tile.transform.localScale * 11.8f;  //ローカルスケールを一致させるため12倍
            obj.transform.rotation = tile.transform.rotation;
            obj.transform.position = tile.transform.position + new Vector3(0,0.07f,0);
            
            MeshFilter filter = obj.AddComponent<MeshFilter>();
            MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
            MeshCollider collider = obj.AddComponent<MeshCollider>();
            
            Mesh mesh = tile.GetComponent<MeshFilter>().sharedMesh;
            
            
            filter.sharedMesh = mesh;
            collider.sharedMesh = mesh;
            renderer.material = material;
        }
        ChangeMaterial();

    }

    void ChangeMaterial(){
        int count = 0;
        foreach(GameObject tile in tiles){
            switch(tile.GetComponent<Tile>().tileInfo.tileType){
                case EnumDefinitions.TileType.START:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.START];
                    break;
                case EnumDefinitions.TileType.MONEY:
                if(tile.GetComponent<Tile>().tileInfo.money_delta > 0){
                    transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[5];
                }else{
                    transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.MONEY];
                }
                    break;
                case EnumDefinitions.TileType.EMPLOY:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.EMPLOY];
                    break;
                case EnumDefinitions.TileType.JOB_RANKUP:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.JOB_RANKUP];
                    break;
                case EnumDefinitions.TileType.MARRY:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.MARRY];
                    break;
                case EnumDefinitions.TileType.BIRTH:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.BIRTH];
                    break;
                case EnumDefinitions.TileType.TREASURE:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.TREASURE];
                    break;
                case EnumDefinitions.TileType.HOUSING:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.HOUSING];
                    break;
                case EnumDefinitions.TileType.INSURANCE:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.INSURANCE];
                    break;
                case EnumDefinitions.TileType.SETTLE:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.SETTLE];
                    break;
                case EnumDefinitions.TileType.GOAL:
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL];
                    break;
            }
            if(tile.GetComponent<Tile>().tileInfo.isRed){
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL + 1];
            }
            if(tile.GetComponent<Tile>().tileInfo.isMustStop){
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL + 2];
            }
            if(tile.GetComponent<Tile>().tileInfo.isSalaryTile){
                transform.GetChild(count).GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL + 3];
            }
            count++;
            // int random = Random.Range(0,materials.Length);
            // tile.GetComponent<MeshRenderer>().material = materials[random];
        }
    }
}
