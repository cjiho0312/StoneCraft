using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneStorageLogUI : MonoBehaviour
{
    Canvas StoneStorageLogCanvas;
    [SerializeField] GameObject OriginListPanel;

    List<GameObject> list;

    [SerializeField] Sprite LimestoneSprite;
    string[] StoneNameString = {"Limestone", ".", ".", ".", ".", "." };

    private void Awake()
    {
        StoneStorageLogCanvas = GetComponent<Canvas>();
        list = new List<GameObject>();
    }

    private void Start()
    {
        StoneStorageLogCanvas.enabled = false;
    }

    private void Update()
    {
        if (StoneStorageLogCanvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                CloseStoneStorageLogUI();
            }
        }
    }

    public void OpenStoneStorageLogUI()
    {
        StoneStorageLogUpdate();
        Pause.Instance.OnPause();
        Cursor.lockState = CursorLockMode.Locked;
        StoneStorageLogCanvas.enabled = true;
    }

    public void CloseStoneStorageLogUI()
    {
        DestroyLog();
        Pause.Instance.OffPause();
        StoneStorageLogCanvas.enabled = false;
    }

    private void StoneStorageLogUpdate()
    {
        int [] Stones = StoneStorageManager.Instance.GetStonesArrayfromStorage();

        for (int i = 0; i < Stones.Length; i++)
        {
            if (Stones[i] == 0)
            {
                continue;
            }

            GameObject L = Instantiate(OriginListPanel);
            L.SetActive(true);
            L.transform.SetParent(OriginListPanel.GetComponentInParent<Transform>(), false);

            list.Add(L);

            Text t = L.GetComponentInChildren<Text>();
            string temp = StoneNameString[i] + " * " + Stones[i].ToString();
            t.text = temp;
        }
    }

    private void DestroyLog()
    {
        foreach (GameObject L in list)
        {
            if (L != null)
            {
                Destroy(L);
            }
        }

        list.Clear();
    }
}
