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
    private SerializedProperty applyOnSelfProperty;

    private SerializedProperty isStatIncrementedProperty;
    private SerializedProperty effectTypeProperty;
    private SerializedProperty statAffectedProperty;
    private SerializedProperty effectLifeTimeProperty;
    private SerializedProperty timeBetweenApplyEffectProperty;
    private SerializedProperty isValueInPercentageProperty;

    private SerializedProperty statWhatToSeeProperty;
    private SerializedProperty isTheOwnerStatProperty;
    //private SerializedProperty useOnlyPermanentStatVariationsProperty;

    private SerializedProperty valueInPercentageProperty;
    private SerializedProperty valueProperty;


    private void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("effectName");
        descriptionProperty = serializedObject.FindProperty("description");
        applyOnSelfProperty = serializedObject.FindProperty("applyOnSelf");

        isStatIncrementedProperty = serializedObject.FindProperty("isStatIncremented");
        effectTypeProperty = serializedObject.FindProperty("effectType");
        statAffectedProperty = serializedObject.FindProperty("statAffected");
        effectLifeTimeProperty = serializedObject.FindProperty("effectLifeTime");
        timeBetweenApplyEffectProperty = serializedObject.FindProperty("timeBetweenApplyEffect");
        isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");

        statWhatToSeeProperty = serializedObject.FindProperty("statWhatToSee");
        isTheOwnerStatProperty = serializedObject.FindProperty("isTheOwnerStat");
        //useOnlyPermanentStatVariationsProperty = serializedObject.FindProperty("useOnlyPermanentStatVariations");

        valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");
        valueProperty = serializedObject.FindProperty("value");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nameProperty);
        EditorGUILayout.PropertyField(descriptionProperty);
        EditorGUILayout.PropertyField(applyOnSelfProperty);

        EditorGUILayout.PropertyField(effectTypeProperty);
        EditorGUILayout.PropertyField(effectLifeTimeProperty);
        EditorGUILayout.PropertyField(statAffectedProperty);
        EditorGUILayout.PropertyField(isStatIncrementedProperty);
        EditorGUILayout.PropertyField(isValueInPercentageProperty);

        if (isValueInPercentageProperty.boolValue)
        {
            EditorGUILayout.PropertyField(statWhatToSeeProperty);
            EditorGUILayout.PropertyField(isTheOwnerStatProperty);
            //EditorGUILayout.PropertyField(useOnlyPermanentStatVariationsProperty);
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