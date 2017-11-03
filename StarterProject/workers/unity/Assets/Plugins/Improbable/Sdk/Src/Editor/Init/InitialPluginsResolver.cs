using UnityEditor;

namespace Improbable.Unity.EditorTools.Build
{
// This pre-compiler logic is for internal purposes only. It prevents the code from being executed in the UnitySDK project.
#if !IMPROBABLE_UNITY_SDK_PROJECT
    [InitializeOnLoad]
    class InitialPluginsResolver
    {
        static InitialPluginsResolver()
        {
            UnityPlayerBuilders.RetrievePluginForCurrentPlatform();
            AssetDatabase.Refresh();
        }
    }
#endif
}