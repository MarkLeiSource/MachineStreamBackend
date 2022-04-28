namespace MachineStreamBackend.Utils
{
    public static class EventStatusValidateUtil
    {
        private static HashSet<string> _status = new HashSet<string>()
        {
            "idle",
            "running",
            "error",
            "finished",
            "repaired",
        };

        public static bool IsStatusValid(string status)
        {
            return _status.Contains(status);
        }
    }
}
