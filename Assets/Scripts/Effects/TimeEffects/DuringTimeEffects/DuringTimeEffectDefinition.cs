using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "DuringTimeEffectDefinition", menuName = "Effects/DuringTimeEffects/During Time Effect Definition")]
public class DuringTimeEffectDefinition : DuringTimeEffect
{
   
}

[CustomEditor(typeof(DuringTimeEffectDefinition))]
public class DuringTimeEffectDefinitionEditor : Editor
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

    private void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("myName");
        descriptionProperty = serializedObject.FindProperty("description");

        buffDebuffTypeProperty = serializedObject.FindProperty("buffDebuffType");
        effectTimeProperty = serializedObject.FindProperty("effectTime");
        statAffectedProperty = serializedObject.FindProperty("statAffected");
        isPositiveProperty = serializedObject.FindProperty("isPositive");
        isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");

        valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");
        valueProperty = serializedObject.FindProperty("value");

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


        serializedObject.ApplyModifiedProperties();
    }
}
