using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetManager : MonoBehaviour
{
    private static AssetBundle animators = null;
    public static AssetBundle Animators{
        get{
            if(animators == null){
                animators = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "animators"));
            }
            return animators;
        }
    }

    private static AssetBundle meshes = null;
    public static AssetBundle Meshes{
        get{
            if(meshes == null){
                meshes = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "meshes"));
            }
            return meshes;
        }
    }

    public static void LoadAllAssets(){
        if(Animators && Meshes)
        return;
    }

    public static T LoadAsset<T>(string name) where T : UnityEngine.Object{
        Debug.Log("looking for object " + name + " of type " + typeof(T).Name);
        #if UNITY_EDITOR
        string[] objectsFound = UnityEditor.AssetDatabase.FindAssets(name + " t:" + typeof(T).Name);
        if(objectsFound != null && objectsFound.Length > 0){
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(objectsFound[0]));
        }else{
            Debug.Log("object " + name + " not found");
            return null;
        }
        #endif
        string typeName = typeof(T).Name.ToLower();
        AssetBundle bundle = null;
        switch(typeName){
            case "animators":
                bundle = Animators;
                break;
            case "meshes":
                bundle = Meshes;
                break;
            default:
                Debug.Log("Access to bundle " + typeName + " is not implemented");
                return null;
        }
        return bundle.LoadAsset<T>(name);
    }

    public static UnityEngine.Object LoadAsset(string name, System.Type type){
        Debug.Log("looking for object " + name + " of type " + type.Name);
        #if UNITY_EDITOR
        string[] objectsFound = UnityEditor.AssetDatabase.FindAssets(name + " t:" + type.Name);
        if(objectsFound != null && objectsFound.Length > 0){
            return UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(objectsFound[0]), type);
        }else{
            Debug.Log("object " + name + " not found");
            return null;
        }
        #endif
        string typeName = type.Name.ToLower();
        AssetBundle bundle = null;
        switch(typeName){
            case "animators":
                bundle = Animators;
                break;
            case "meshes":
                bundle = Meshes;
                break;
            default:
                Debug.Log("Access to bundle " + typeName + " is not implemented");
                return null;
        }
        return bundle.LoadAsset(name, type);
    }

    public static void UnloadAssets<T>(bool shouldDestroy = true){
        #if UNITY_EDITOR
        return;
        #endif
        
        switch(typeof(T).ToString().ToLower()){
            case "animators":
                animators.Unload(shouldDestroy);
                animators = null;
                break;
            case "meshes":
                meshes.Unload(shouldDestroy);
                meshes = null;
                break;
            default:
                Debug.Log("Access to bundle " + typeof(T).ToString().ToLower() + " is not implemented");
                return;

        }
    }

    public static void UnloadAllAssets(bool shouldDestroy = true){
        #if UNITY_EDITOR
        return;
        #endif
        AssetBundle.UnloadAllAssetBundles(shouldDestroy);
        animators = null;
        meshes = null;
    }
}
