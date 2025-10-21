using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector director;
    public Transform playerSpawn;
    public GameObject hud;
    public PlayerController player;

    void Start()
    {
        if (GameStateManager.I.cutsceneSeen)
        {
            EndCutsceneImmediate();
            return;
        }
        if (director != null)
        {
            director.stopped += OnDirectorStopped;
            director.Play();
        }
        else
        {
            Debug.LogWarning("PlayableDirector não atribuído em CutsceneController.");
            EndCutscene();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SkipCutscene();
        }
    }

    public void SkipCutscene()
    {
        if (director == null) return;
        director.time = director.duration;
        director.Evaluate();
        director.Stop();
        EndCutscene();
    }

    void OnDirectorStopped(PlayableDirector pd)
    {
        EndCutscene();
    }

    public void EndCutsceneImmediate()
    {
        player.EnableControl();
        hud.SetActive(true);
    }

    public void EndCutscene()
    {
        GameStateManager.I.cutsceneSeen = true;

        if (playerSpawn != null && player != null)
            player.transform.position = playerSpawn.position;
        else
            Debug.LogWarning("playerSpawn ou player não atribuído em CutsceneController.");

        player.EnableControl();
        hud.SetActive(true);

        GameStateManager.I.objectiveActive = true;
    }

    public void Signal_ShowSubtitle(string content)
    {
        SubtitleManager sm = null;

#if UNITY_2023_1_OR_NEWER
            sm = Object.FindFirstObjectByType<SubtitleManager>();
#else
        sm = Object.FindObjectOfType<SubtitleManager>();
#endif

        if (sm != null)
        {
            sm.Show(content, 3f);
        }
        else
        {
            Debug.LogWarning("SubtitleManager não encontrado para mostrar legenda: " + content);
        }
    }
}
