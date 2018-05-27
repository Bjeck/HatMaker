using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Collider))]
public class HatColorStation : MonoBehaviour
{
    private const float SPRAY_TIME = 5f;
    private const float COLOR_CHANGE_PER_SEC = 0.2f;

    public Transform redSlider;
    public Transform greenSlider;
    public Transform blueSlider;


    public Color stationColor;

    private Collider collider;
    private List<Renderer> renderersOnStation;

    [SerializeField] Renderer rend;

    public void Start()
    {
        renderersOnStation = new List<Renderer>();
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    public void OnInteract()
    {

        StopCoroutine("Spray");
        StartCoroutine("Spray");
    }

    private void Update()
    {
        stationColor = new Color(redSlider.localPosition.x + 0.5f, greenSlider.localPosition.x + 0.5f, blueSlider.localPosition.x + 0.5f);
        rend.material.color = stationColor;
    }


    private IEnumerator Spray()
    {
        //print("start coroutine");
        float t = 0f;
       // while(t < SPRAY_TIME)
       // {
         //   t += Time.deltaTime;
            yield return null;

            foreach(Renderer renderer in renderersOnStation)
            {
            //float r = stationColor.r - renderer.material.color.r;
            //float g = stationColor.g - renderer.material.color.g;
            //float b = stationColor.b - renderer.material.color.b;

            //int rSign = Math.Sign(r);
            //int gSign = Math.Sign(g);
            //int bSign = Math.Sign(b);

            //float rChange = -rSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;
            //float gChange = -gSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;
            //float bChange = -bSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;

            //if(Math.Sign(renderer.material.color.r) != Math.Sign(renderer.material.color.r + rChange))
            //{
            //    rChange = 0f;
            //}
            //if (Math.Sign(renderer.material.color.g) != Math.Sign(renderer.material.color.g + gChange))
            //{
            //    gChange = 0f;
            //}
            //if (Math.Sign(renderer.material.color.b) != Math.Sign(renderer.material.color.b + bChange))
            //{
            //    bChange = 0f;
            //}

            //renderer.material.color = new Color(r + rChange, g + gChange, b + bChange, renderer.material.color.a);
            renderer.material.color = stationColor;

            }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer renderer = other.gameObject.GetComponent<Renderer>();
        if(renderer != null)
        {
            //print("adding " + other.gameObject.name);
            renderersOnStation.Add(renderer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer renderer = other.gameObject.GetComponent<Renderer>();
        if(renderer != null)
        {
            //print("removing " + other.gameObject.name);
            renderersOnStation.Remove(renderer);
        }
    }
}
