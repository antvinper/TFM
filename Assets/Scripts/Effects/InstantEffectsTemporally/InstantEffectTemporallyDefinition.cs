using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InstantEffectTemporally", menuName = "Effects/InstantEffects/Instant Effect Temporally Definition")]
public class InstantEffectTemporallyDefinition : InstantEffectTemporally
{
    
}


[CustomEditor(typeof(InstantEffectTemporallyDefinition))]
public class InstantEffectTemporallyDefinitionEditor : Editor
{
    private SerializedProperty nameProperty;
    private SerializedProperty descriptionProperty;
    private SerializedProperty applyOnSelfProperty;

    private SerializedProperty isStatIncrementedProperty;
    private SerializedProperty statAffectedInTargetProperty;

    private SerializedProperty isValueInPercentageProperty;
    private SerializedProperty statWhatToSeeProperty;
    private SerializedProperty isTheOwnerStatProperty;
    private SerializedProperty valueInPercentageProperty;

    private SerializedProperty valueProperty;

    private void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("effectName");
        descriptionProperty = serializedObject.FindProperty("description");
        applyOnSelfProperty = serializedObject.FindProperty("applyOnSelf");

        isStatIncrementedProperty = serializedObject.FindProperty("isStatIncremented");
        statAffectedInTargetProperty = serializedObject.FindProperty("statAffected");

        isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");
        statWhatToSeeProperty = serializedObject.FindProperty("statWhatToSee");
        isTheOwnerStatProperty = serializedObject.FindProperty("isTheOwnerStat");
        valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");

        valueProperty = serializedObject.FindProperty("value");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nameProperty);
        EditorGUILayout.PropertyField(descriptionProperty);
        EditorGUILayout.PropertyField(applyOnSelfProperty);
        EditorGUILayout.PropertyField(isStatIncrementedProperty);
        EditorGUILayout.PropertyField(statAffectedInTargetProperty);
        EditorGUILayout.PropertyField(isValueInPercentageProperty);

        if (isValueInPercentageProperty.boolValue)
        {
            EditorGUILayout.PropertyField(statWhatToSeeProperty);
            EditorGUILayout.PropertyField(isTheOwnerStatProperty);
            EditorGUILayout.PropertyField(valueInPercentageProperty);
        }
        else
        {
            EditorGUILayout.PropertyField(valueProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
