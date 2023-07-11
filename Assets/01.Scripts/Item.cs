using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    public AudioClip clip;
    public int effID;
    public GameObject aura;

    enum OrbType
    {
        None,
        Fire,
        Electric
    }


    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;

    }

    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);


        switch (data.itemType)
        {
            case ItemData.ItemType.Orb:
                if (data.itemId == (int)OrbType.None)
                    textDesc.text = string.Format(data.itemDesc);
                else if (data.itemId == (int)OrbType.Fire)
                    textDesc.text = string.Format(data.itemDesc, data.damages[level]);
                else if (data.itemId == (int)OrbType.Electric)
                    textDesc.text = string.Format(data.itemDesc, data.counts[level]);
                break;

            case ItemData.ItemType.Barrel:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            case ItemData.ItemType.Engine:
                textDesc.text = string.Format(data.itemDesc, data.damages[level]);
                break;

            case ItemData.ItemType.Core:
                textDesc.text = string.Format(data.itemDesc);
                break;
            case ItemData.ItemType.Trash:
                textLevel.text = "";
                textDesc.text = string.Format(data.itemDesc);
                break;
        }

    }

    public void OnClick()
    {
        SoundManager.instance.ButtonClickSound();

        switch (data.itemType)
        {
            case ItemData.ItemType.Orb:
                for (int index = 0; index < GameManager.instance.effectPool.pool.Length; index++)
                {
                    if (data.effect == GameManager.instance.effectPool.pool[index])
                    {
                        effID = index;
                        break;
                    }
                }

                clip = data.hitSound;
                aura = data.aura;


                ShootBul.Instance.addDamege = data.damages[level];
                ShootBul.Instance.addPiercingNum = data.counts[level];

                break;
            case ItemData.ItemType.Barrel:
                ShootBul.Instance.shootSpeed = ShootBul.Instance.baseShootSpeed * (1 - data.damages[level]);

                break;
            case ItemData.ItemType.Engine:
                ShootBul.Instance.numBullets = (int)data.damages[level];

                break;
            case ItemData.ItemType.Core:
                if (level <= GameManager.instance.bulletPool.pool.Length - 2)
                    ShootBul.Instance.bulltype++;


                if (level == GameManager.instance.bulletPool.pool.Length - 2)
                    gameObject.SetActive(false);

                break;
            case ItemData.ItemType.Trash:
                GameManager.instance.player.nowHp += 5;

                break;

        }

        level++;
        GameManager.instance.uiLevelUp.Hide();

        if (level == data.damages.Length)
        {
            transform.parent = GameObject.Find("UI/FullLevel").transform;
        }
    }
}
