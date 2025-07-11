namespace OdysseyPatch
{
    public static class Debug
    {
        public static void Log(object message)
        {
#if DEBUG
            Verse.Log.Message($"[{OdysseyPatchMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
