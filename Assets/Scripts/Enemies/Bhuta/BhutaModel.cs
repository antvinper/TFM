using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhutaModel : EnemyModel
{
    List<TeleportPoint> TeleportPoints;
    int ActualFloorColliderInstanceId;
    BoxCollider FloorCollider;

    public List<TeleportPoint> teleportPoints { get => TeleportPoints; }
    public int actualFloorColliderInstanceId { get => ActualFloorColliderInstanceId; set => ActualFloorColliderInstanceId = value; }
    public BoxCollider floorCollider { get => FloorCollider; set => FloorCollider = value; }

    public void SetTeleportPoints()
    {
        TeleportPoints = new List<TeleportPoint>(GetComponentsInChildren<TeleportPoint>());
    }
}
