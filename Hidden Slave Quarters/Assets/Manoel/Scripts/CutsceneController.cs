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
        director.stopped += OnDirectorStopped;
        director.Play();
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
  
        player.transform.position = playerSpawn.position;
        player.EnableControl();
        hud.SetActive(true);

        GameStateManager.I.objectiveActive = true;
    }

  
    public void Signal_ShowSubtitle(string content)
    {
        SubtitleManager sm = FindObjectOfType<SubtitleManager>();
        sm.Show(content, 3f);
    }
}
