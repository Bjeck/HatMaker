using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Collider))]
public class HatColorStation : MonoBehaviour
{
    private const float SPRAY_TIME = 5f;
    private const float COLOR_CHANGE_PER_SEC = 0.2f;

    public Color stationColor;

    private Collider collider;
    private List<Renderer> renderersOnStation;

    public void Start()
    {
        renderersOnStation = new List<Renderer>();
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    public void OnInteract()
    {
        StopCoroutine(Spray());
        StartCoroutine(Spray());
    }

    private IEnumerator Spray()
    {
        float t = 0f;
        while(t < SPRAY_TIME)
        {
            t += Time.deltaTime;
            yield return null;

            foreach(Renderer renderer in renderersOnStation)
            {
                float r = stationColor.r - renderer.material.color.r;
                float g = stationColor.g - renderer.material.color.g;
                float b = stationColor.b - renderer.material.color.b;

                int rSign = Math.Sign(r);
                int gSign = Math.Sign(g);
                int bSign = Math.Sign(b);

                float rChange = -rSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;
                float gChange = -gSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;
                float bChange = -bSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;

                if(Math.Sign(renderer.material.color.r) != Math.Sign(renderer.material.color.r + rChange))
                {
                    rChange = 0f;
                }
                if (Math.Sign(renderer.material.color.g) != Math.Sign(renderer.material.color.g + gChange))
                {
                    gChange = 0f;
                }
                if (Math.Sign(renderer.material.color.b) != Math.Sign(renderer.material.color.b + bChange))
                {
                    bChange = 0f;
                }

                renderer.material.color = new Color(r + rChange, g + gChange, b + bChange, renderer.material.color.a);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer renderer = other.gameObject.GetComponent<Renderer>();
        Debug.Log("Adding GameObject " + other.gameObject.name + " with material: " + renderer);
        if(renderer != null)
        {
            renderersOnStation.Add(renderer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer renderer = other.gameObject.GetComponent<Renderer>();
        Debug.Log("Removing GameObject " + other.gameObject.name + " with material: " + renderer);
        if(renderer != null)
        {
            renderersOnStation.Remove(renderer);
        }
    }
}
