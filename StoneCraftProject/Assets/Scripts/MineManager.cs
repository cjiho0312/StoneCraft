using System.Collections;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    public void StartMining(MineBase mine)
    {
        var playerManager = PlayerManager.Instance;

        float d = mine.durability;
        StoneData s = mine.GetStoneType();

        playerManager.ChangePlayerState(PlayerState.MINNING);

        StartCoroutine(Mining());

        playerManager.ChangePlayerState(PlayerState.IDLE);
    }

    IEnumerator Mining()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
        }
    }
}
