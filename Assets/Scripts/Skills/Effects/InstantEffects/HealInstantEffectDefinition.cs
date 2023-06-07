using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "HealInstantEffect", menuName = "Effects/InstantEffects/Heal Effect Definition ")]
public class HealInstantEffectDefinition : HealInstantEffect
{
}

[CustomEditor(typeof(HealInstantEffectDefinition))]
public class HealInstantEffectDefinitionEditor : Editor
{
    private SerializedProperty nameProperty;
    private SerializedProperty descriptionProperty;

    private SerializedProperty statAffectedProperty;
    private SerializedProperty isValueInPercentageProperty;

    private SerializedProperty valueInPercentageProperty;

    private SerializedProperty statWhatToSeeProperty;
    private SerializedProperty isTheOwnerStatProperty;

    private void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("name");
        descriptionProperty = serializedObject.FindProperty("description");

        statAffectedProperty = serializedObject.FindProperty("statAffected");
        isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");

        valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");

        statWhatToSeeProperty = serializedObject.FindProperty("statWhatToSee");
        isTheOwnerStatProperty = serializedObject.FindProperty("isTheOwnerStat");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nameProperty);
        EditorGUILayout.PropertyField(descriptionProperty);
        EditorGUILayout.PropertyField(statAffectedProperty);
        EditorGUILayout.PropertyField(isValueInPercentageProperty);

        if (isValueInPercentageProperty.boolValue)
        {
            EditorGUILayout.PropertyField(valueInPercentageProperty);
            EditorGUILayout.PropertyField(statWhatToSeeProperty);
            EditorGUILayout.PropertyField(isTheOwnerStatProperty);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
