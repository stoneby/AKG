using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Loom : MonoBehaviour
{
    public class DelayedQueueItem
    {
        public float Time;
        public Action Action;
        public string Name;
    }

    #region Private Fields

    private int count;
    private readonly Action[] toRun = new Action[4000];

    private readonly List<Action> actions = new List<Action>();
    private readonly List<DelayedQueueItem> delayed = new List<DelayedQueueItem>();

    private static Loom instance;

    public static Loom Instance
    {
        get { return instance ?? (instance = FindObjectOfType<Loom>()); }
    }

    #endregion

    #region Public Methods

    public void RemoveDelay(string delay)
    {
        lock (delayed)
        {
            for (var i = 0; i < delayed.Count; ++i)
            {
                var delayItem = delayed[i];
                if (delayItem.Name.Equals(delay))
                {
                    delayed.RemoveAt(i);
                }
            }
        }
    }

    public static void QueueOnMainThread(Action action, string name)
    {
        QueueOnMainThread(action, 0, name);
    }

    public static void QueueOnMainThread(Action action, float time, string name)
    {
        if (Math.Abs(time - 0) > float.Epsilon || !string.IsNullOrEmpty(name))
        {
            lock (Instance.delayed)
            {
                DelayedQueueItem existing = null;
                if (!string.IsNullOrEmpty(name))
                    existing = Instance.delayed.FirstOrDefault(d => d.Name == name);
                if (existing != null)
                {
                    existing.Time = Time.time + time;
                    return;
                }
                var queueItem = new DelayedQueueItem();
                queueItem.Name = name;
                queueItem.Time = Time.time + time;
                queueItem.Action = action;
                Instance.delayed.Add(queueItem);
            }
        }
        else
        {
            lock (Instance.actions)
            {
                Instance.actions.Add(action);
            }
        }

    }

    /// <summary>
    /// Queues an action on the main thread
    /// </summary>
    /// <param name='action'>
    /// The action to execute
    /// </param>
    public static void QueueOnMainThread(Action action)
    {
        QueueOnMainThread(action, 0f);
    }

    /// <summary>
    /// Queues an action on the main thread after a delay
    /// </summary>
    /// <param name='action'>
    /// The action to run
    /// </param>
    /// <param name='time'>
    /// The amount of time to delay
    /// </param>
    public static void QueueOnMainThread(Action action, float time)
    {
        QueueOnMainThread(action, time, null);
    }

    /// <summary>
    /// Runs an action on another thread
    /// </summary>
    /// <param name='action'>
    /// The action to execute on another thread
    /// </param>
    public static void RunAsync(Action action)
    {
        var t = new Thread(RunAction)
        {
            Priority = System.Threading.ThreadPriority.Normal
        };
        t.Start(action);
    }

    #endregion

    #region Private Methods

    private static void RunAction(object action)
    {
        ((Action)action)();
    }

    #endregion

    #region Mono

    void Update()
    {
        if (!Application.isPlaying)
        {
            actions.Clear();
            delayed.Clear();
            return;
        }

        var count = Mathf.Min(actions.Count, 4000);
        lock (actions)
        {
            actions.CopyTo(0, toRun, 0, count);
            if (count == actions.Count)
                actions.Clear();
            else
                actions.RemoveRange(0, count);
        }
        for (var i = 0; i < count; i++)
        {
            try
            {
                toRun[i]();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        lock (delayed)
        {
            count = 0;
            for (var i = delayed.Count - 1; i >= 0 && count < 3999; i--)
            {
                if (!(delayed[i].Time <= Time.time))
                {
                    continue;
                }
                toRun[count++] = delayed[i].Action;
                delayed.RemoveAt(i);
            }
        }

        for (var i = 0; i < count; i++)
        {
            try
            {
                toRun[i]();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    void OnLevelWasLoaded()
    {
        actions.Clear();
        delayed.Clear();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }
    }

    #endregion
}