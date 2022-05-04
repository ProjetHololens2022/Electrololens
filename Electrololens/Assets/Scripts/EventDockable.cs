using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeAgent
{ CONSUMER, PRODUCER }

public class EventDockable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public TypeAgent type;
}
