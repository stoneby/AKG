using UnityEngine;

public class Pauser : Singleton<Pauser>
{
    private bool paused;

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            paused = !paused;
        }

        Time.timeScale = paused ? 0 : 1;
    }
}
