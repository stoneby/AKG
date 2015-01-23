using System.Collections;
using UnityEngine;

/// <summary>
/// Ping test utilities.
/// </summary>
public class PingTest : Singleton<PingTest>
{
    #region Public Fields

    public string IpAddress;

    public bool HasConnection { get; set; }

    #endregion

    #region Private Fields

    private const float MaxTime = 2.0f;

    #endregion

    #region Public Methods

    public IEnumerator TestConnection()
    {
        return TestConnection(IpAddress);
    }

    /// <summary>
    /// Test whether player has connect to network.
    /// </summary>
    /// <param name="ipAddress">IP address to ping through</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator TestConnection(string ipAddress)
    {
        var timeTaken = 0.0F;
        var testPing = new Ping(ipAddress);
        while (!testPing.isDone)
        {
            timeTaken += Time.deltaTime;
            if (timeTaken > MaxTime)
            {
                // if time has exceeded the max time, break out and return false
                HasConnection = false;
                break;
            }
            yield return null;
        }
        if (timeTaken <= MaxTime)
        {
            HasConnection = true;
        }
    }

    #endregion
}
