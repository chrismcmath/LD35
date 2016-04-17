using UnityEngine;
using System.Collections;

namespace GB.Core {
    public class RuntimeInitializeOnLoad : MonoBehaviour {
        public const string GLOBAL_NAME = "Global";
        public const string GLOBAL_PATH = "core/global";

        static GameObject _Global;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] 
        static void GlobalSetup() 
        {
            Rf.Core.Global[] g = FindObjectsOfType<Rf.Core.Global>();
            if (_Global == null && g.Length == 0) {
                _Global = GameObject.Instantiate(Resources.Load(GLOBAL_PATH) as GameObject);
                _Global.name = GLOBAL_NAME;
            }
        }
    }
}
