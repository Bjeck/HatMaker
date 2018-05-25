using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoryStation : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> AccessoryPrefabs;

    public List<SpawnPointInfo> SpawnPoints;

    [Header("Object Links")]
    public Transform AccessoryDisplayParent;
    public Text DisplayText;

    [Header("Materials")]
    public Material GreenMaterial;
    public Material RedMaterial;

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
        StartCoroutine(SpawnSequence());
    }

    //Called via UnityEvent from Next Accessory Button
    public void SelectNextAccessory()
    {
        selectedAccessoryIdx++;

        if (selectedAccessoryIdx > AccessoryPrefabs.Count - 1)
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

    private SpawnPointInfo SelectRandomSpawnPoint()
    {
        int rndIdx = Random.Range(0, SpawnPoints.Count);
        return SpawnPoints[rndIdx];
    }

    private IEnumerator SpawnSequence()
    {
        int totalIterations = 10;
        float interval = 0.1f;

        int currentIteration = 0;

        while (currentIteration < totalIterations)
        {
            SpawnPointInfo spawnPointInfo = SelectRandomSpawnPoint();
            SetLEDSelected(spawnPointInfo);

            currentIteration++;

            yield return new WaitForSeconds(interval);
        }

        SpawnPointInfo selectedSpawnPoint = SelectRandomSpawnPoint();
        SetLEDSelected(selectedSpawnPoint);

        yield return new WaitForSeconds(interval);

        selectedSpawnPoint.SpawnLED.material = RedMaterial;

        yield return new WaitForSeconds(interval / 2);

        selectedSpawnPoint.SpawnLED.material = GreenMaterial;

        yield return new WaitForSeconds(interval);

        selectedSpawnPoint.SpawnLED.material = RedMaterial;     //... wow this is really good code ( °_ʖ °)

        yield return new WaitForSeconds(interval / 2);

        selectedSpawnPoint.SpawnLED.material = GreenMaterial;

        yield return new WaitForSeconds(interval);

        selectedSpawnPoint.SpawnLED.material = RedMaterial;

        yield return new WaitForSeconds(interval / 2);

        selectedSpawnPoint.SpawnLED.material = GreenMaterial;

        yield return new WaitForSeconds(1f);

        GameObject newAccessory = Instantiate(AccessoryPrefabs[selectedAccessoryIdx], selectedSpawnPoint.SpawnPoint.position, Quaternion.identity) as GameObject;
    }

    private void SetLEDSelected(SpawnPointInfo spawnPoint)
    {
        spawnPoint.SpawnLED.material = GreenMaterial;

        var otherSpawnPoints = SpawnPoints.FindAll(p => p != spawnPoint);
        otherSpawnPoints.ForEach(p => p.SpawnLED.material = RedMaterial);
    }

    [System.Serializable]
    public class SpawnPointInfo
    {
        public Transform SpawnPoint;
        public Renderer SpawnLED;
    }

}
