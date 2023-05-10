using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BAM
{
    public class Loader : MonoBehaviour
    {
        private void Start()
        {
            ReflexSceneManager.LoadScene("BAM", LoadSceneMode.Single, builder =>
            {
                // This deferred descriptor will run just before Greet.unity SceneScope installers
                builder.AddInstance("Test");
            });
        }
    }
}
