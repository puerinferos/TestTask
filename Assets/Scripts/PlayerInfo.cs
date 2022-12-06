using System;
using UnityEngine;

public static class PlayerInfo
{
    public static Action<int> OnTemporaryChanged;
    public static Action<int> OnPermanentChanged;

    public static int TemporaryCollected
    {
        get => PlayerPrefs.GetInt(nameof(TemporaryCollected));
        set
        {
            PlayerPrefs.SetInt(nameof(TemporaryCollected), value);
            OnTemporaryChanged?.Invoke(value);
        }
    }

    public static int PermanentCollected
    {
        get => PlayerPrefs.GetInt(nameof(PermanentCollected));
        set
        {
            PlayerPrefs.SetInt(nameof(PermanentCollected), value);
            OnPermanentChanged?.Invoke(value);
        }
    }
}