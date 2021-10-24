using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HouseData
{
    public string name;
    public int price;  //値段
    public int area;    //面積
    public string address; //住所
    public int age;    //築年数
    public EnumDefinitions.HouseStructure structure;
    public int capacity;   //入居可能人数
    public int story;  //階数
    public Sprite sprite;

    public HouseData(string name, int price, string address, int age, EnumDefinitions.HouseStructure structure, int capacity, int story, Sprite sprite, int area){
        this.name = name;
        this.price = price;
        this.address = address;
        this.age = age;
        this.structure = structure;
        this.capacity = capacity;
        this.story = story;
        this.sprite = sprite;
        this.area = area;
    }
}
