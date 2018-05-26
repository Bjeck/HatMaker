using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSlider : MonoBehaviour {

    public float posLimit;
    public Vector3 direction; //this is fx 1,0,0

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos;

        pos = new Vector3(Mathf.Clamp(transform.localPosition.x, direction.x * (-posLimit), direction.x * posLimit),
                          50,
                          Mathf.Clamp(transform.localPosition.z, direction.z * (-posLimit), direction.z * posLimit));


        transform.localPosition = pos;

    }
}
