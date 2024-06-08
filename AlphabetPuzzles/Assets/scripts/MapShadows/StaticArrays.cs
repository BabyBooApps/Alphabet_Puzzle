using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StaticArrays 
{
	//public static List<string> alpsList;
 //   public static List<string> lowercaseList;
 //   public static List<string> alphaImgList;

    public static List<int> alphabetSpriteList;

    //public static List<int> lowercaseSpriteList;
    //public static List<int> uppercaseSpriteList;
    //public static List<int> uppercaseImageSpriteList;

    public static string typecase = "uppercase";
    public static string orderType = "sequence";

    //public static System.Random random = null;

	public static Vector3[] initialPositions =new Vector3[3]{new Vector3(-8.25f,4f,-0.5f),new Vector3(-8.25f,1f,-0.5f),new Vector3(-8.25f,-2f,-0.5f)};

	public static float leftX = 0f;
	public static float aspect = 0f;
    public const string LOWERCASE = "lowercase";
    public const string UPPERCASE = "uppercase";
    public static string SEQUENCE = "sequence";
    public static string SHUFFLE = "shuffle";
    public static string TYPECASE = "typecase";
    public static string ORDERTYPE = "ordertype";
}
