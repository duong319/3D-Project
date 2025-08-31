using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CountryFlag
{
    public string countryCode;
    public Sprite flagSprite;
}

public class CountryFlagManager : MonoBehaviour
{
    public static CountryFlagManager Instance;

    public List<CountryFlag> countryFlags = new List<CountryFlag>();
    private Dictionary<string, Sprite> flagDict;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }

        flagDict = new Dictionary<string, Sprite>();
        foreach (var cf in countryFlags)
        {
            if (!flagDict.ContainsKey(cf.countryCode.ToUpper()))
                flagDict.Add(cf.countryCode.ToUpper(), cf.flagSprite);
        }
    }

    public Sprite GetFlagSprite(string countryCode)
    {
        if (string.IsNullOrEmpty(countryCode)) return null;
        countryCode = countryCode.ToUpper();

        if (flagDict.ContainsKey(countryCode))
            return flagDict[countryCode];

        return flagDict[countryCode];
    }
}
