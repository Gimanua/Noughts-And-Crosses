using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses
{
    static class SaveHandler
    {
        public static bool Save<T>(T data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
                File.WriteAllText($"{Environment.CurrentDirectory}\\saveFile.dat", json);
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
