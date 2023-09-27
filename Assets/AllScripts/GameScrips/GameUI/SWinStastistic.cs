using System;
using System.Diagnostics;
using Unity.VisualScripting;

public class SWinStastistic
{
    internal protected int playerFellSum;
    internal protected int percentEnemiesSum;
    internal protected int bridgeBrokeSum;
    
    internal protected SWinStastistic (int playerMovement, int enemiesSum, int killedEnemies, int brokeBridge)
    {
        playerFellSum = playerMovement;
        percentEnemiesSum = (int)(((double)killedEnemies / enemiesSum) * 100);
        bridgeBrokeSum = brokeBridge;
        
    }
}
