using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneStorageLogUI : MonoBehaviour
{
    Canvas StoneStorageLogCanvas;
    [SerializeField] GameObject OriginListPanel;

    List<GameObject> list;

    [SerializeField] Sprite LimestoneSprite;

    bool canCloseUI;

    private void Awake()
    {
        StoneStorageLogCanvas = GetComponent<Canvas>();
        list = new List<GameObject>();
    }

    private void Start()
    {
        canCloseUI = false;
        StoneStorageLogCanvas.enabled = false;
    }

    private void Update()
    {
        if (!StoneStorageLogCanvas.enabled) return;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            CloseStoneStorageLogUI();
        }
        
    }

    public void OpenStoneStorageLogUI()
    {
        canCloseUI = false;

        PlayerInteract.Instance.SetCanInteract(false);
        StoneStorageLogUpdate();
        Pause.Instance.OnPause();

        Cursor.lockState = CursorLockMode.Locked; // OnPause에서 마우스 풀어버려서 다시 잠굼
        Cursor.visible = false;

        StoneStorageLogCanvas.enabled = true;
        StartCoroutine(Delay());
    }

    public void CloseStoneStorageLogUI()
    {
        if (!canCloseUI) { return; }

        DestroyLog();
        Pause.Instance.OffPause();
        StoneStorageLogCanvas.enabled = false;

        PlayerInteract.Instance.SetCanInteract(true);
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
            
            string temp = NameStrData.Instance.GetStoneName(i) + " * " + Stones[i].ToString();
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

    IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();
        canCloseUI = true;
    }
}
