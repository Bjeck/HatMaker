using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForANewBeginningYay : MonoBehaviour
{
    public Vector3 axis;
    public float speed;

    private Transform trn;

	private void Awake()
	{
        trn = transform;
	}

	private void Update()
	{
        trn.RotateAround(trn.position, axis, speed * Time.deltaTime);
	}
}
