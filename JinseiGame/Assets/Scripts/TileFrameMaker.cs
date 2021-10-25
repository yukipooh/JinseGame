using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFrameMaker : MonoBehaviour
{
    MeshFilter meshFilter;
    Mesh mesh = new Mesh();


    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.sharedMesh;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
