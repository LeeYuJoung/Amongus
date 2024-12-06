using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterMove : CharacterMove
{
    public void CompleteSpawn()
    {
        if (isOwned)
        {
            isMoveable = true;
        }
    }
}
