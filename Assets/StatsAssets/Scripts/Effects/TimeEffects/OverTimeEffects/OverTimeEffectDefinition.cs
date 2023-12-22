using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CompanyStats
{
    [CreateAssetMenu(fileName = "OverTimeEffectDefinition", menuName = "Effects/TimeEffects/Over Time Effect Definition")]
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
        private SerializedProperty statPartAffectedProperty;
        private SerializedProperty effectLifeTimeProperty;
        private SerializedProperty timeBetweenApplyEffectProperty;
        private SerializedProperty isValueInPercentageProperty;
        private SerializedProperty isInfiniteProperty;

        private SerializedProperty statWhatToSeeProperty;
        private SerializedProperty isTheOwnerStatProperty;
        private SerializedProperty statWhatToSeeStatPartAffectedProperty;

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
            statPartAffectedProperty = serializedObject.FindProperty("statPart");
            effectLifeTimeProperty = serializedObject.FindProperty("effectLifeTime");
            timeBetweenApplyEffectProperty = serializedObject.FindProperty("timeBetweenApplyEffect");
            isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");
            isInfiniteProperty = serializedObject.FindProperty("isInfinite");

            statWhatToSeeProperty = serializedObject.FindProperty("statWhatToSee");
            isTheOwnerStatProperty = serializedObject.FindProperty("isTheOwnerStat");
            statWhatToSeeStatPartAffectedProperty = serializedObject.FindProperty("statWhatToSeeStatPart");

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
            EditorGUILayout.PropertyField(isInfiniteProperty);

            if (!isInfiniteProperty.boolValue)
            {
                EditorGUILayout.PropertyField(effectLifeTimeProperty);
            }
            //EditorGUILayout.PropertyField(effectLifeTimeProperty);
            EditorGUILayout.PropertyField(statAffectedProperty);
            EditorGUILayout.PropertyField(statPartAffectedProperty);
            EditorGUILayout.PropertyField(isStatIncrementedProperty);
            EditorGUILayout.PropertyField(isValueInPercentageProperty);

            if (isValueInPercentageProperty.boolValue)
            {
                EditorGUILayout.PropertyField(statWhatToSeeProperty);
                EditorGUILayout.PropertyField(isTheOwnerStatProperty);
                EditorGUILayout.PropertyField(statWhatToSeeStatPartAffectedProperty);
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
}

