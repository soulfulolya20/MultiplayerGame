using System.Collections.Generic;

public class GameManager
{
    private static List<PlayerBase> _players = new List<PlayerBase>();
    
     
    public static void RegisterPlayer(PlayerBase player)
    {
        _players.Add(player);
    }

    public static int CountPlayers()
    {
        return _players.Count;
    }

    public static void RemovePlayer(PlayerBase player)
    {
        _players.Remove(player);
    }
}