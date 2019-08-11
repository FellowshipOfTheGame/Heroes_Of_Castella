using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetManager
{
    private static List<AssetBundle> bundleList;
    private static List<AssetBundle> BundleList {
        get{
            if(bundleList == null)
                bundleList = new List<AssetBundle>();
            return bundleList;
        }
    }

    private static AssetBundle skill = null;
    private static AssetBundle Skill{
        get{
            if(skill == null || !BundleList.Contains(skill)){
                skill = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "skill"));
                BundleList.Add(skill);
            }
            return skill;
        }
    }

    public static void LoadAllBundles(){
        #if UNITY_EDITOR
        return;
        #else
        if(Skill)
        return;
        #endif
    }

    public static void LoadAllAssets(){
        #if UNITY_EDITOR
        return;
        #else
        LoadAllBundles();
        if(Skill)
            LoadBundleAssets<Skill>();
        return;
        #endif
    }

    private static AssetBundle GetBundle(string typeName){
        AssetBundle bundle = null;
        switch(typeName){
            case "skill":
                bundle = Skill;
                break;
            default:
                Debug.Log("Access to bundle " + typeName + " is not implemented");
                break;
        }
        return bundle;
    }

    public static List<T> LoadBundleAssets<T>() where T : UnityEngine.Object{
        #if UNITY_EDITOR
        string[] objectsFound = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);
        List<T> newList = new List<T>();
        foreach(string guid in objectsFound){
            newList.Add(UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid)));
        }
        return newList;
        #else
        string typeName = typeof(T).Name.ToLower();
        AssetBundle bundle = GetBundle(typeName);
        return (bundle == null)? null : new List<T>(bundle.LoadAllAssets<T>());
        #endif
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
        #else
        string typeName = typeof(T).Name.ToLower();
        AssetBundle bundle = GetBundle(typeName);
        return (bundle == null)? null : bundle.LoadAsset<T>(name);
        #endif
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
        #else
        string typeName = type.Name.ToLower();
        AssetBundle bundle = GetBundle(typeName);
        return (bundle == null)? null : bundle.LoadAsset(name, type);
        #endif
    }

    public static void UnloadAssets<T>(bool shouldDestroy = true){
        #if UNITY_EDITOR
        return;
        #else
        
        string typeName = typeof(T).Name.ToLower();
        AssetBundle bundle = GetBundle(typeName);
        bundle.Unload(shouldDestroy);
        BundleList.Remove(bundle);
        #endif
    }

    public static void UnloadAllAssets(bool shouldDestroy = true){
        #if UNITY_EDITOR
        return;
        #else
        foreach(AssetBundle bundle in BundleList){
            bundle.Unload(shouldDestroy);
        }
        BundleList.Clear();
        #endif
    }
}
