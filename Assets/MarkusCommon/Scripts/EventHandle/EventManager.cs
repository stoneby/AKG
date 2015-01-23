using System;
using System.Collections.Generic;

/// <summary>
/// Event manager which is singleton.
/// </summary>
public class EventManager
{
    #region Singleton

    private static EventManager instance;

    public static EventManager Instance
    {
        get { return instance ?? (instance = new EventManager()); }
    }

    #endregion

    #region Delegates

    public delegate void EventDelegate<in T>(T e) where T : GameEvent;

    #endregion

    #region Private Fields

    private readonly Dictionary<Type, Delegate> delegates = new Dictionary<Type, Delegate>();

    #endregion

    #region Public Methods

    /// <summary>
    /// Add listener to specific type of game event.
    /// </summary>
    /// <typeparam name="T">Subclass of GameEvent</typeparam>
    /// <param name="listener">Listener who is interested with the GameEvent</param>
    public void AddListener<T>(EventDelegate<T> listener) where T : GameEvent
    {
        Delegate d;
        if (delegates.TryGetValue(typeof(T), out d))
        {
            delegates[typeof(T)] = Delegate.Combine(d, listener);
        }
        else
        {
            delegates[typeof(T)] = listener;
        }
    }

    /// <summary>
    /// Remove listener to specific type of game event.
    /// </summary>
    /// <typeparam name="T">Subclass of GameEvent</typeparam>
    /// <param name="listener">Listener who is interested with the GameEvent</param>
    public void RemoveListener<T>(EventDelegate<T> listener) where T : GameEvent
    {
        Delegate d;
        if (!delegates.TryGetValue(typeof (T), out d))
        {
            return;
        }

        var currentDel = Delegate.Remove(d, listener);

        if (currentDel == null)
        {
            delegates.Remove(typeof(T));
        }
        else
        {
            delegates[typeof(T)] = currentDel;
        }
    }

    /// <summary>
    /// Post event to all listeners who register to it.
    /// </summary>
    /// <typeparam name="T">Type of GameEvent</typeparam>
    /// <param name="e">Subclass of GameEvent</param>
    public void Post<T>(T e) where T : GameEvent
    {
        if (e == null)
        {
            throw new ArgumentNullException("e");
        }

        Delegate d;
        if (!delegates.TryGetValue(typeof (T), out d))
        {
            Logger.LogWarning("There is no listeners registered with event - " + e.GetType().Name);
            return;
        }

        var callback = d as EventDelegate<T>;
        if (callback != null)
        {
            callback(e);
        }
    }

    #endregion
}