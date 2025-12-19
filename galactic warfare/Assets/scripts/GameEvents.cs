using System;

public static class GameEvents
{
    public static Action<int> OnHealthChanged;
    public static Action<int> OnLivesChanged;
    public static Action<int> OnAmmoChanged;
    public static Action<string> OnWeaponChanged;
    public static Action<int> OnScoreChanged;
}
