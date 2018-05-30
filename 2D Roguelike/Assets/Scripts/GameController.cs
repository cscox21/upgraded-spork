using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
    }

}
