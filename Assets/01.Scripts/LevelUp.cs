using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void Show()
    {
        SoundManager.instance.LevelUpSound();

        NextSelect();
        rect.transform.localScale = Vector3.one;
        Time.timeScale = 0f;
        GameManager.instance.gameState = GameManager.GameState.stay;
    }

    public void Hide()
    {
        rect.transform.localScale = Vector3.zero;
        Time.timeScale = 1f;
        GameManager.instance.gameState = GameManager.GameState.ing;
    }

    public void NextSelect()
    {
        items = transform.GetComponentsInChildren<Item>(true);

        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        if (items.Length > 3)
        {
            int[] rand = new int[3];
            while (true)
            {
                rand[0] = Random.Range(0, items.Length);
                rand[1] = Random.Range(0, items.Length);
                rand[2] = Random.Range(0, items.Length);

                if (rand[0] != rand[1] && rand[1] != rand[2] && rand[2] != rand[0])
                    break;
            }

            foreach (int index in rand)
            {
                items[index].gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var item in items)
            {
                item.gameObject.SetActive(true);
            }
        }
        
    }
}
