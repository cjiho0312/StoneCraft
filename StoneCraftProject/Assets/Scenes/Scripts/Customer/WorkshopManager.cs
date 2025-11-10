using System.Collections;
using UnityEngine;

public class WorkshopManager : MonoBehaviour
{
    static public WorkshopManager Instance;
    public bool isWorkshopOpen;
    [SerializeField] Shelf shelf;
    [SerializeField] GameObject Customer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        isWorkshopOpen = false;
    }

    public void OpenWorkshop()
    {
        isWorkshopOpen = true;
        shelf.StopInteract();
        StartCoroutine(WelcomeCustomers());
    }

    public void CloseWorkshop()
    {
        isWorkshopOpen = false;
        shelf.CanInteract();
    }

    IEnumerator WelcomeCustomers()
    {
        while (isWorkshopOpen)
        {
            GameObject C = Instantiate(Customer);
            C.SetActive(true);
            yield return new WaitForSeconds(7f);
        }

        yield return null;
    }

    public bool IsExistSculpture()
    {
        return shelf.isExistSculpure();
    }

    public void SellSculputure()
    {
        if (!shelf.isExistSculpure())
        {
            return;
        }

        ShelfSlot S = shelf.ChoiceOneSculpture();
        int V = S.SculptureData.value;
        PlayerManager.Instance.AddMoney(V);
        GuideTextManager.Instance.MakeGuide(GuideSub.GETITEM, "Coin + " + V.ToString());
        S.DestroySculpture();

        AudioManager.Instance.PlayCoinSound();
    }
}
