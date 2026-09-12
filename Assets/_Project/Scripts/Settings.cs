/// <summary>
/// Хранит основные настройки управления и движения игрока.
/// </summary>
public static class Settings
{
    /// <summary>
    /// Скорость движения игрока.
    /// </summary>
    public static float PlayerSpeed = 7.5f;

    /// <summary>
    /// Сила прыжка игрока.
    /// </summary>
    public static float PlayerJumpForce = 2f;

    /// <summary>
    /// Чувствительность горизонтального обзора.
    /// </summary>
    public static float HorizontalSensitivity = 5f;

    /// <summary>
    /// Чувствительность вертикального обзора.
    /// </summary>
    public static float VerticalSensitivity = 5f;

    /// <summary>
    /// Минимальный угол вертикального обзора.
    /// </summary>
    public static float MinVerticalAngle = -90f;

    /// <summary>
    /// Максимальный угол вертикального обзора.
    /// </summary>
    public static float MaxVerticalAngle = 90f;
}