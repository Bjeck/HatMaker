using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Collider))]
public class HatColorStation : MonoBehaviour
{
    private const float SPRAY_TIME = 5f;
    private const float COLOR_CHANGE_PER_SEC = 70f;

    public Color stationColor;

    private Collider collider;
    private List<Material> materialsOnStation;

    public void Start()
    {
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

            foreach(Material material in materialsOnStation)
            {
                float r = stationColor.r - material.color.r;
                float g = stationColor.g - material.color.g;
                float b = stationColor.b - material.color.b;

                int rSign = Math.Sign(r);
                int gSign = Math.Sign(g);
                int bSign = Math.Sign(b);

                float rChange = rSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;
                float gChange = gSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;
                float bChange = bSign * COLOR_CHANGE_PER_SEC * Time.deltaTime;

                if(Math.Sign(material.color.r) != Math.Sign(material.color.r + rChange))
                {
                    rChange = 0f;
                }
                if (Math.Sign(material.color.g) != Math.Sign(material.color.g + gChange))
                {
                    gChange = 0f;
                }
                if (Math.Sign(material.color.b) != Math.Sign(material.color.b + bChange))
                {
                    bChange = 0f;
                }

                material.color = new Color(r, g, b, material.color.a);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Material material = other.GetComponent<Material>();
        if(material != null)
        {
            materialsOnStation.Add(material);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Material material = other.GetComponent<Material>();
        if(material != null)
        {
            materialsOnStation.Remove(material);
        }
    }
}
