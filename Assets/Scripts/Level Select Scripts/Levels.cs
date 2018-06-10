using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Levels {

	// public int index;
	// public string name;

	// public List<Object> levels = new List<Object>();
	public LevelData[] levels;

	// public static Levels CreateFromJSON(string json)
    // {   
    //     return JsonUtility.FromJson<Levels>(json);
    // }
}

[System.Serializable]
public class LevelData {
	public string index;
	public string name;

	public LevelOptions[] options;
}

[System.Serializable]
public class LevelOptions {
	public CurtainOptions curtainOption;
}