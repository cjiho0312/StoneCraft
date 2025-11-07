using System.Collections.Generic;
using UnityEngine;

public class SculptingStoneDisplay : MonoBehaviour
{
    // 프로토타입용

    [SerializeField] Material LimestoneM;

    [SerializeField] GameObject S1000; // defalt Stone
    [SerializeField] GameObject S1001;
    [SerializeField] GameObject S1002;
    [SerializeField] GameObject S1003;

    GameObject currentObject;
    float rotateSpeed = 30f;
    bool isRotate;

    private void Start()
    {
        currentObject = null;
        isRotate = false;
    }

    private void Update()
    {
        if (!isRotate) { return; }

        if (currentObject != null)
        {
            currentObject.transform.Rotate(0f, rotateSpeed * Time.unscaledDeltaTime, 0f, Space.World);
        }
    }

    public void DisplaySculpture(int SculptureStepIndex)
    {
        if (SculptureStepIndex == 0)
        {
            Display(S1000);
        }
        else if (SculptureStepIndex == 1)
        {
            Display(S1001);
        }
        else if (SculptureStepIndex == 2)
        {
            Display(S1002);
        }
        else if (SculptureStepIndex == 3)
        {
            Display(S1003);
        }
        else if (SculptureStepIndex == -1)
        {
            Delete(currentObject);
        }
    }

    private void Display(GameObject g)
    {
        if (currentObject != null)
        {
            Delete(currentObject);
        }

        GameObject S = Instantiate(g);
        S.transform.parent = gameObject.transform;
        S.transform.localPosition = Vector3.zero;
        S.transform.localScale = new Vector3(40f, 40f, 40f);

        MeshRenderer meshRenderer = S.GetComponent<MeshRenderer>();
        meshRenderer.material = LimestoneM;

        currentObject = S;
    }

    public void RotateSculpture()
    {
        isRotate = true;
    }

    public GameObject GetSculpturePrefab()
    {
        // 프로토타입용

        return S1003;
    }

    private void Delete(GameObject g)
    {
        if (currentObject == null) return;
        Destroy(g);
        currentObject = null;
    }

    public void StopDisplay()
    {
        if (currentObject == null) return;

        Destroy(currentObject);
        currentObject = null;
        isRotate = false;
    }
}
