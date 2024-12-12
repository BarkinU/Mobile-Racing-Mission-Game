using UnityEngine;
using System.Collections.Generic;


public static class PlayerPrefsExtra
{

	#region Color -----------------------------------------------------------------------------------------

	public static Color GetColor (string key)
	{
		return Get<Color> (key, new Color (0f, 0f, 0f, 0f));
	}

	public static Color GetColor (string key, Color defaultValue)
	{
		return Get<Color> (key, defaultValue);
	}

	public static void SetColor (string key, Color value)
	{
		Set (key, value);
	}

	#endregion

	#region Bool -----------------------------------------------------------------------------------------

	public static bool GetBool (string key)
	{
		return (PlayerPrefs.GetInt (key, 0) == 1);
	}

	public static bool GetBool (string key, bool defaultValue)
	{
		return (PlayerPrefs.GetInt (key, (defaultValue ? 1 : 0)) == 1);
	}

	public static void SetBool (string key, bool value)
	{
		PlayerPrefs.SetInt (key, (value ? 1 : 0));
	}

	#endregion

	
	//Generic template ---------------------------------------------------------------------------------------

	static T Get<T> (string key, T defaultValue)
	{
		return JsonUtility.FromJson <T> (PlayerPrefs.GetString (key, JsonUtility.ToJson (defaultValue)));
	}

	static void Set<T> (string key, T value)
	{
		PlayerPrefs.SetString (key, JsonUtility.ToJson (value));
	}

}
