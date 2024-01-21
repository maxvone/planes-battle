namespace CodeBase.Extensions
{
    public static class FloatExtension
    {
        public static int ToMilliseconds(this float seconds) => 
            (int)(seconds * 1000);
    }
}