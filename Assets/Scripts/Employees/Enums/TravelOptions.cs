namespace Employees.Enums
{
    public enum TravelOptions
    {
        None = 0x00,

        Run = 0x01,
        Horse = 0x02,
        Guard = 0x04,

        Catapult = 0x08,
        Portal = 0x10,

        GraphMovement = Run | Horse | Guard,
        TeleportMovement = Catapult | Portal,
    }
}