using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
public enum TypeAgent
{ CONSUMER, PRODUCER }

public enum TypeEvent
{ CDM, EUROVISION, CENTRALEHS, PENURIECHARBON, PENURIEEAU, PENURIESOLEIL, PENURIEVENT, HEUREPOINTE, GREVE, TDT}

public class EventDockable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public TypeAgent type;
    public TypeEvent typeEvent;


}
