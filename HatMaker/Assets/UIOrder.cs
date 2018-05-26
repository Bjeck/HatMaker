using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrder : MonoBehaviour {

    [SerializeField] List<Sprite> hatsprites = new List<Sprite>();
    Dictionary<HatTypeName, Sprite> hattypeSprite = new Dictionary<HatTypeName, Sprite>();

    [SerializeField] List<Sprite> accessorysprites = new List<Sprite>();
    Dictionary<HatAccessory, Sprite> accessorytypeSprite = new Dictionary<HatAccessory, Sprite>();

    public Image playerBackground;
    public Image image;
    public Image scale;
    public Image color;
    public Image accessoryImage;
    public Text text;
    public Order order;

    float timer;

    private void Awake()
    {
        for (int i = 0; i < hatsprites.Count; i++)
        {
            hattypeSprite.Add((HatTypeName)System.Enum.Parse(typeof(HatTypeName), hatsprites[i].name), hatsprites[i]);
        }
        for (int i = 0; i < accessorysprites.Count; i++)
        {
            accessorytypeSprite.Add((HatAccessory)System.Enum.Parse(typeof(HatAccessory), accessorysprites[i].name), accessorysprites[i]);
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        text.text = "" + timer;

    }

    public void Setup(Order order)
    {
        this.order = order;
        if (hattypeSprite.ContainsKey(order.hattributes.type))
        {
            image.sprite = hattypeSprite[order.hattributes.type];
        }

        if (accessorytypeSprite.ContainsKey(order.hattributes.accessories[0]))
        {
            accessoryImage.sprite = accessorytypeSprite[order.hattributes.accessories[0]];
        }


        scale.fillAmount = Map(order.hattributes.size.x, 1.5f, 5f, 0f, 1f);
        color.color = order.hattributes.color;
        playerBackground.color = order.player.color;

        timer = order.timelimit;
    }



    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
