using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointHover : MonoBehaviour {

    [SerializeField] Text text;

    public void ShowNumber(int nr)
    {
        text.text = "" + nr;
    }


}
