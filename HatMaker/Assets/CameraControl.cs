using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    Vector3 startPos;
    Vector3 startRot;
    [SerializeField] Vector3 scoreboardPos;
    [SerializeField] Vector3 scoreboardRot;


	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startRot = transform.rotation.eulerAngles;
	}
	

    public void MoveToScoreBoard()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        Quaternion rot = new Quaternion();
        rot.eulerAngles = scoreboardRot;

        while(Vector3.Distance(transform.position,scoreboardPos) > 0)
        {
            transform.position = Vector3.Lerp(transform.position, scoreboardPos, Time.deltaTime * 5f);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5f);

            if(Vector3.Distance(transform.position, scoreboardPos) < 1)
            {
                transform.position = scoreboardPos;
            }

            yield return null;

        }
    }

}
