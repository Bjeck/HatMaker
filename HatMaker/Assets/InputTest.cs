using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // print(hInput.GetAxis("j1Horizontal"));

        if(hInput.GetButtonDown("j1Use")){
            print("use!");
        }
       
    }
}
