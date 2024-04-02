using Medica.Uk.TechnicalDemonstration.DataModels;
using Medica.Uk.TechnicalDemonstration.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;

namespace Medica.Uk.TechnicalDemonstration.DataAccess.Mappers
{
    public class JsonDataMapper<T> : DataMapper<T> where T : class
    {

        public JsonDataMapper()
        {
            _dataStore = new Dictionary<string, T>();
        }

        public override IEnumerable<T>? MapData(Stream stream)
        {
            using var streamReader = new StreamReader(stream,Encoding.UTF8,true);
            const int bufferSize = 1024;
            char[] buffer = new char[bufferSize];
            StringBuilder fileContent = new StringBuilder();
            int charsRead;
            while ((charsRead = streamReader.Read(buffer, 0, bufferSize)) > 0)
            {
                fileContent.Append(buffer, 0, charsRead);
            }

            string jsonContent = fileContent.ToString().Trim();

            using var jsonReader = new JsonTextReader(new StringReader(fileContent.ToString()));
            var jsonSerializer = new JsonSerializer();

            try
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.StartObject)
                    {
                        var record = jsonSerializer.Deserialize<T>(jsonReader);
                        if (record != null)
                        {
                            if (!_dataStore.TryAdd(GetKey(record), record))
                            {
                                LoggerHelper.Logger.Warning("Skipped duplicate key {Key} while mapping JSON data", GetKey(record));
                            }
                        }
                        else
                        {
                            jsonReader.Skip();
                            LoggerHelper.Logger.Debug("Skipped null record while mapping JSON data");
                        }
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                LoggerHelper.Logger.Error(ex, "An error occurred while deserializing JSON data");
            }

            return _dataStore?.Values;

        }
    }
}