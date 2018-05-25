using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoryStation : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> AccessoryPrefabs;

    public List<Transform> SpawnPoints;

    [Header("Object Links")]
    public Transform AccessoryDisplayParent;
    public Text DisplayText;


    public GameObject SelectedAccessory
    {
        get { return AccessoryPrefabs[selectedAccessoryIdx]; }
    }

    #region private variables
    private int selectedAccessoryIdx = 0;
    private GameObject CurrentDisplayAccessory;
	#endregion

	private void Awake()
	{
        SetAccessoryDisplay();
	}

	//Called via UnityEvent from Spawn Button
	public void SpawnAccessory()
    {
        Transform spawnLocation = SelectRandomSpawnPoint();

        GameObject newAccessory = Instantiate(AccessoryPrefabs[selectedAccessoryIdx], spawnLocation.position, Quaternion.identity) as GameObject;
    }

    //Called via UnityEvent from Next Accessory Button
    public void SelectNextAccessory()
    {
        selectedAccessoryIdx++;

        if (selectedAccessoryIdx > AccessoryPrefabs.Count-1)
        {
            selectedAccessoryIdx = 0;
        }

        SetAccessoryDisplay();
    }

    private void SetAccessoryDisplay()
    {
        DisplayText.text = selectedAccessoryIdx.ToString();

        if (CurrentDisplayAccessory != null)
        {
            Destroy(CurrentDisplayAccessory);
        }

        CurrentDisplayAccessory = Instantiate(SelectedAccessory, AccessoryDisplayParent.position, Quaternion.identity, AccessoryDisplayParent);
    }

    private Transform SelectRandomSpawnPoint()
    {
        int rndIdx = Random.Range(0, SpawnPoints.Count);
        return SpawnPoints[rndIdx];
    }

}
