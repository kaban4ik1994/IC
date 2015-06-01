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
    }
}