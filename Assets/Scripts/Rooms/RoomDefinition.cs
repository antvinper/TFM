using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDefinition", menuName = "TFM/Room/NormalRoomDefinition")]
public class RoomDefinition : Room
{
    
}

#if UNITY_EDITOR
[CustomEditor(typeof(RoomDefinition))]
public class RoomDefnitionEditor: Editor
{
    private SerializedProperty roomTypeProperty;
    private SerializedProperty rupeesMinAmountProperty;
    private SerializedProperty soulFragmentsMinAmountProperty;
    private SerializedProperty healMinAmountProperty;
    private SerializedProperty healSkillProperty;

    private void OnEnable()
    {
        roomTypeProperty = serializedObject.FindProperty("roomType");
        rupeesMinAmountProperty = serializedObject.FindProperty("rupeesMinAmount");
        soulFragmentsMinAmountProperty = serializedObject.FindProperty("soulFragmentsMinAmount");
        healMinAmountProperty = serializedObject.FindProperty("healMinAmount");
        healSkillProperty = serializedObject.FindProperty("healSkill");
    }

    public override void OnInspectorGUI()
    {
        Room room = (Room)target;
        serializedObject.Update();

        EditorGUILayout.PropertyField(roomTypeProperty);
        if (roomTypeProperty.intValue.Equals((int)RoomTypesEnum.NORMAL_ROOM) ||
            roomTypeProperty.intValue.Equals((int)RoomTypesEnum.HIGH_ROOM) ||
            roomTypeProperty.intValue.Equals((int)RoomTypesEnum.BOSS_ROOM))
        {
            room.hasReward = true;
        }
        else
        {
            room.hasReward = false;
        }

        if (room.hasReward)
        {
            EditorGUILayout.PropertyField(rupeesMinAmountProperty);
            EditorGUILayout.PropertyField(soulFragmentsMinAmountProperty);
            EditorGUILayout.PropertyField(healMinAmountProperty);
            EditorGUILayout.PropertyField(healSkillProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
