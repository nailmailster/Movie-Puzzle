using UnityEngine;

public static class SettingsData
{
    private static bool soundOn = true;
    public static bool SoundOn
    {
        get { return soundOn; }
        set { soundOn = value; }
    }

    private static bool sfxOn = true;
    public static bool SfxOn
    {
        get { return sfxOn; }
        set { sfxOn = value; }
    }

    private static bool assistantOn = false;
    public static bool AssistantOn
    {
        get { return assistantOn; }
        set { assistantOn = value; }
    }

    private static bool win = false;
    public static bool Win
    {
        get { return win; }
        set { win = value; }
    }
}