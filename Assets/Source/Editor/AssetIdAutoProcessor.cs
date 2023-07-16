using Source.Configs;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Configs.Editor
{
    public class AssetIdAutoProcessor : AssetPostprocessor
    {
        private const string ScriptableObjectExtension = ".asset";

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string importedAsset in importedAssets)
            {
                if (importedAsset.EndsWith(ScriptableObjectExtension))
                {
                    string assetName = System.IO.Path.GetFileNameWithoutExtension(importedAsset);
                    string assetPath = importedAsset.Replace(Application.dataPath, "Assets");
                    ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
                    if (scriptableObject is BaseConfig item)
                    {
                        if (item.id != assetName)
                        {
                            item.id = assetName;
                            EditorUtility.SetDirty(scriptableObject);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }
            }
        }
    }
}