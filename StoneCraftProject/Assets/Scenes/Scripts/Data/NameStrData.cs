using UnityEngine;

public class NameStrData : MonoBehaviour
{
    public static NameStrData Instance;

    string[] StoneNameString = { "Limestone", "Marble", ".", ".", ".", "." };
    string[] ToolGradeString = { "Wood", "Stone", "Iron", "Diamond" };

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public string GetStoneName(int index)
    {
        return StoneNameString[index];
    }

    public string GetToolGrade(int index)
    {
        return ToolGradeString[index];
    }
}
