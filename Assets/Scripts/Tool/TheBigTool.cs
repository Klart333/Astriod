#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TheBigTool : EditorWindow
{

    private SpawnerData spawnerData;
    private ShootingData shootingData;

    // Spawner
    private SerializedObject spawnerObject;
    private SerializedProperty SpawnRate;
    private SerializedProperty MinSize;
    private SerializedProperty MaxSize;
    private SerializedProperty MinForce;
    private SerializedProperty MaxForce;
    private SerializedProperty Angle;
    private SerializedProperty RateIncrease;
    private SerializedProperty ForceIncrease;

    // Shooting
    private SerializedObject shootingObject;
    private SerializedProperty FireRate;
    private SerializedProperty BulletSpeed;

    private void OnEnable()
    {
        spawnerData = (SpawnerData)Resources.Load("Datas/SpawnerData");
        shootingData = (ShootingData)Resources.Load("Datas/ShootingData");

        // Spawner
        spawnerObject = new SerializedObject(spawnerData);
        SpawnRate = spawnerObject.FindProperty("SpawnRate");
        MinSize = spawnerObject.FindProperty("MinSize");
        MaxSize = spawnerObject.FindProperty("MaxSize");
        MinForce = spawnerObject.FindProperty("MinForce");
        MaxForce = spawnerObject.FindProperty("MaxForce");
        Angle = spawnerObject.FindProperty("Angle");
        RateIncrease = spawnerObject.FindProperty("RateIncrease");
        ForceIncrease = spawnerObject.FindProperty("ForceIncrease");

        // Shooting
        shootingObject = new SerializedObject(shootingData);
        FireRate = shootingObject.FindProperty("FireRate");
        BulletSpeed = shootingObject.FindProperty("BulletSpeed");
    }

    private void OnGUI()
    {
        spawnerObject.Update();
        shootingObject.Update();

        GUILayout.Label("Spawner Data", EditorStyles.whiteLargeLabel);
        // Spawner
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label("Spawning Stats", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(SpawnRate);
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(MinSize);
                EditorGUILayout.PropertyField(MaxSize);
            }

            GUILayout.Space(10);
            GUILayout.Label("Launching", EditorStyles.boldLabel);
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(MinForce);
                EditorGUILayout.PropertyField(MaxForce);
            }
            EditorGUILayout.PropertyField(Angle);

            GUILayout.Space(10);
            GUILayout.Label("Scaling (10s)", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(RateIncrease);
            EditorGUILayout.PropertyField(ForceIncrease);
        }
        spawnerObject.ApplyModifiedProperties();

        GUILayout.Space(25);
        GUILayout.Label("Shooting Data", EditorStyles.whiteLargeLabel);
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label("Shooting", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(FireRate);
            EditorGUILayout.PropertyField(BulletSpeed);
        }
        shootingObject.ApplyModifiedProperties();
    }


    [MenuItem("Tools/Tool")]
    public static void Thing()
    {
        GetWindow<TheBigTool>("Control panel");
    }
}
#endif 
