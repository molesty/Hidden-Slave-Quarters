using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager I { get; private set; }

    public bool cutsceneSeen = false;
    public Vector3 playerSpawnPoint;
    public bool objectiveActive = false;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }
}
