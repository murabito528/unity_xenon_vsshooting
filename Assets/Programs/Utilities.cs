using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities// : MonoBehaviour
{
    //static public Vector3 one;

    static public class vec
    {
        static public Vector3 one = Vector3.one;
        static public Vector3 zero = Vector3.zero;
        static public Vector3 forward = Vector3.forward;
        static public Vector3 right = Vector3.right;
        static public Vector3 up = Vector3.up;
    }

    static public class color
    {
        static public Color black = new Color(0, 0, 0, 1);
        static public Color white = new Color(1, 1, 1, 1);
        static public Color expred = new Color(1, 0.46f, 0.46f, 1);
        static public Color expblue = new Color(0.54f, 1, 0.97f, 1);
    }
}
