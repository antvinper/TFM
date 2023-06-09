using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "OverTimeEffectDefinition", menuName = "Effects/OverTimeEffects/Over Time Effect Definition")]
public class OverTimeEffectDefinition : OverTimeEffect
{
    
}


[CustomEditor(typeof(OverTimeEffectDefinition))]
public class OverTimeEffectDefinitionEditor : Editor
{
    private SerializedProperty nameProperty;
    private SerializedProperty descriptionProperty;

    private SerializedProperty buffDebuffTypeProperty;
    private SerializedProperty effectTimeProperty;
    private SerializedProperty statAffectedProperty;
    private SerializedProperty isPositiveProperty;
    private SerializedProperty isValueInPercentageProperty;

    private SerializedProperty valueInPercentageProperty;
    private SerializedProperty valueProperty;

    private SerializedProperty timeBetweenApplyEffectProperty;

    private void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("name");
        descriptionProperty = serializedObject.FindProperty("description");

        buffDebuffTypeProperty = serializedObject.FindProperty("buffDebuffType");
        effectTimeProperty = serializedObject.FindProperty("effectTime");
        statAffectedProperty = serializedObject.FindProperty("statAffected");
        isPositiveProperty = serializedObject.FindProperty("isPositive");
        isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");

        valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");
        valueProperty = serializedObject.FindProperty("value");

        timeBetweenApplyEffectProperty = serializedObject.FindProperty("timeBetweenApplyEffect");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nameProperty);
        EditorGUILayout.PropertyField(descriptionProperty);

        EditorGUILayout.PropertyField(buffDebuffTypeProperty);
        EditorGUILayout.PropertyField(effectTimeProperty);
        EditorGUILayout.PropertyField(statAffectedProperty);
        EditorGUILayout.PropertyField(isPositiveProperty);
        EditorGUILayout.PropertyField(isValueInPercentageProperty);

        if (isValueInPercentageProperty.boolValue)
        {
            EditorGUILayout.PropertyField(valueInPercentageProperty);
        }
        else
        {
            EditorGUILayout.PropertyField(valueProperty);
        }

        EditorGUILayout.PropertyField(timeBetweenApplyEffectProperty);

        serializedObject.ApplyModifiedProperties();
    }
}