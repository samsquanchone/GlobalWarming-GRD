using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuData 
{
    static bool isNewGame = true;


    public static bool GetGameType()
    {
        bool _newGame = isNewGame;

        return _newGame;
    }

    public static void SetGameType(bool _isNewGame)
    {
        isNewGame = _isNewGame;
    }
}
