using UnityEngine;

public enum PickaxeGrade
{
    WOOD,
    STONE,
    IRON,
    DIAMOND
}

public class Pickaxe : MonoBehaviour
{
    public float GetPickaxeSpeed(int Index)
    {
        switch (Index)
        {
            case (int)PickaxeGrade.WOOD:
                return 1f;
            case (int)PickaxeGrade.STONE:
                return 1.3f;
            case (int)PickaxeGrade.IRON:
                return 2f;
            case (int)PickaxeGrade.DIAMOND:
                return 3.3f;
            default:
                return 0f;
        }
    }
}
