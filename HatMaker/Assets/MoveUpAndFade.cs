using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndFade : MonoBehaviour {

    public float time = 4f;
    public float speed = 2;

	// Use this for initialization
	void Start () {
        StartCoroutine(ScaleUp());

    }



    IEnumerator ScaleUp()
    {
        float t = 0;
        while(t < 1)
        {
            transform.localScale = Vector3.one * t;
            t += Time.deltaTime * 4f;
            yield return null;
        }
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        float startTime = time;
        Vector3 orgpos = transform.position;
        float height = 0;

        while(time > 0)
        {
            transform.position = new Vector3(transform.position.x, orgpos.y + height , transform.position.z);
            float scaleNormalized = transform.localScale.x / startTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time / startTime);

            height += Time.deltaTime * speed;
            time -= Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

}
