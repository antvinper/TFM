using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    private RoomController roomController;

    public RoomController RoomController
    {
        set => this.roomController = value;
    }

    public int GetRoomRewards()
    {
        return this.roomController.GetRupeesAmount();
    }
}
