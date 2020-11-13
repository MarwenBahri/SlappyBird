using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;

public class AcademySC : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] agents;
    [HideInInspector]
    public SceneManager[] listManager;

    public void Awake()
    {
        Academy.Instance.OnEnvironmentReset += EnvironmentReset;
    }

    void EnvironmentReset()
    {
        listManager = FindObjectsOfType<SceneManager>();
        foreach (var fa in listManager)
        {
            fa.ResetScene();
        }
    }

}
