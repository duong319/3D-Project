// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("v7CSoKVj+GZ3iFR49nU4Rzbolt9f5YE5Gt7ojN8G3P8D5BldDZFJhY56FxqxKk0Jn30YwtxuwOydymw+lBHBqO4OgToOw8rNgfG5NdKJXbQzgQIhMw4FCimFS4X0DgICAgYDAEIsxsT8/F6Wiar4EjkV8oXXW3glzHpy+IfsNo9N4mo6WCxxcu0iMJnHkaz23tHlJyUZp4Bm7l9tqM/btpCd10a/RN4lTFlEJTSBUOMHQCfZwrMVgRt0MRmT6CAagfPbyl8827KdF0La+NYp0T66z5c/WqZvaoQKlOKZZHRdGEm1IbBV+lC164hLUefdgQIMAzOBAgkBgQICA8Rh32DNc5liIZwo5g6s4tuPHn2iAyL108g3RMK0btmmIN21BgEAAgMC");
        private static int[] order = new int[] { 7,13,10,3,7,7,7,8,12,9,12,11,13,13,14 };
        private static int key = 3;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
