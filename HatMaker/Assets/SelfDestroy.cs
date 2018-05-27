using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    public float waittime;

	// Use this for initialization
	void Start () {
        StartCoroutine(Wait());
	}

    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waittime);

        Destroy(gameObject);
    }

}
