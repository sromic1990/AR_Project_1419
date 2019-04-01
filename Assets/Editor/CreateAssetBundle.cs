#if CREATEASSETBUNDLE
using UnityEditor;
using UnityEngine;

namespace ARProject.Scripts
{
    public class CreateAssetBundle : MonoBehaviour
    {
        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAllAssetBundles(){
            BuildPipeline.BuildAssetBundles("Assets/Bundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

        }
    }
}
#endif
