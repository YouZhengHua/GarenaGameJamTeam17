using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicLevelList", menuName = "Scriptable Objects/MusicLevelList")]
public class MusicLevelList : ScriptableObject
{
    public List<MusicStringOBJ> musicStringOBJs; 
}
