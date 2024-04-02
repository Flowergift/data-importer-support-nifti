using FileHelpers;
using Medica.Uk.TechnicalDemonstration.DataModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Medica.Uk.TechnicalDemonstration.DataAccess.Mappers
{
    public class CsvDataMapper<T> : DataMapper<T> where T : class, new()
    {
        private readonly FileHelperEngine<T> _engine;

        public CsvDataMapper()
        {
            _engine = new FileHelperEngine<T>();

            // Read FileHelperEngine settings from configuration
            var ignoreFirstLines = ConfigurationHelper.Configuration["Csv:FileHelperEngineSettings:IgnoreFirstLines"];
            var ignoreEmptyLines = ConfigurationHelper.Configuration["Csv:FileHelperEngineSettings:IgnoreEmptyLines"];
            var ignoreLastLines = ConfigurationHelper.Configuration["Csv:FileHelperEngineSettings:IgnoreLastLines"];

            // Configure _engine settings
            _engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            _engine.Options.IgnoreFirstLines = int.Parse(ignoreFirstLines);
            _engine.Options.IgnoreEmptyLines = bool.Parse(ignoreEmptyLines);
            _engine.Options.IgnoreLastLines = int.Parse(ignoreLastLines);

            // Save errors if any
            if (_engine.ErrorManager.HasErrors)
                _engine.ErrorManager.SaveErrors("errors.out");

            // Save errors if any
            _dataStore = new Dictionary<string, T>();
        }

        public override IEnumerable<T>? MapData(Stream stream)
        {
            using (var streamReader = new StreamReader(stream, Encoding.UTF8, true, 1024))
            {
                var records = _engine.ReadStream(streamReader);
                _dataStore = records.ToDictionary(r => GetKey(r), r => r);
            }

            return _dataStore.Values;
        }
    }
}