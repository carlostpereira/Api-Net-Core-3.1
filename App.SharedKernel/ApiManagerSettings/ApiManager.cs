using System.Collections.Generic;

namespace App.SharedKernel.ApiManagerSettings
{
    public class ApiManager
    {
        private static Dictionary<string, string> keys = new Dictionary<string, string>();

        public static void AddKey(string name, string value)
        {
            keys.Add(name, value);
        }

        public static string GetKey(string name)
        {
            if (!keys.ContainsKey(name))
                return string.Empty;

            return keys[name];
        }
    }
}
