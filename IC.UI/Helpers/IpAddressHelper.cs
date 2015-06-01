using System;
using System.Linq;

namespace IC.UI.Helpers
{
    public static class IpAddressHelper
    {
        public static string ConvertIpAddressToString(byte[] model)
        {
            return model.Count() != 4 ? string.Empty : String.Format("{0}.{1}.{2}.{3}", model[0], model[1], model[2], model[3]);
        }

        public static byte[] ConvertStringToIpAddress(string model)
        {
            var result = model.Split(new[] { '.' });
            return result.Count() != 4 ? null : new[] { Convert.ToByte(result[0]), Convert.ToByte(result[1]), Convert.ToByte(result[2]), Convert.ToByte(result[3]) };
        }
    }
}