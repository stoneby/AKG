using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPathLayerMapping : MonoBehaviour
{
    /// <summary>
    /// Mapping between prefab path to layer.
    /// </summary>
    public Dictionary<string, WindowGroupType> PathLayerMap { get; set; }

    /// <summary>
    /// Mapping between layer to prefab path.
    /// </summary>
    public Dictionary<WindowGroupType, List<string>> LayerPathMap { get; set; }

    /// <summary>
    /// Mapping between window type to prefab path.
    /// </summary>
    public Dictionary<Type, string> TypePathMap { get; set; }

    /// <summary>
    /// Mapping between prefab path to window type.
    /// </summary>
    public Dictionary<string, Type> PathTypeMap { get; set; }

    public abstract bool Parse();
}
