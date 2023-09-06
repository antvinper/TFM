using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhutaModel : EnemyMutableModel
{
    List<TeleportPoint> teleportPoints;
    int actualFloorColliderInstanceId;
    BoxCollider floorCollider;

    public List<TeleportPoint> TeleportPoints { get => teleportPoints; }
    public int ActualFloorColliderInstanceId { get => actualFloorColliderInstanceId; set => actualFloorColliderInstanceId = value; }
    public BoxCollider FloorCollider { get => floorCollider; set => floorCollider = value; }

    public void SetTeleportPoints(List<TeleportPoint> teleportPoints)
    {
        this.teleportPoints = teleportPoints;
    }
}
