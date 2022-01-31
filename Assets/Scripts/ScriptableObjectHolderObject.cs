using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectHolder", menuName = "ThatBit/ObjectHolder", order = 0)]
public class ScriptableObjectHolderObject : ScriptableObject
{
	public List<ScriptableObject> objectList;
}
