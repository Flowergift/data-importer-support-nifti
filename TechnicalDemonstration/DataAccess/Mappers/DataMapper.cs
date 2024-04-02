using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Medica.Uk.TechnicalDemonstration.DataModels
{
    public abstract class DataMapper<T> where T : class
    {
        protected Dictionary<string, T>? _dataStore; // In-memory data store
        public abstract IEnumerable<T>? MapData(Stream stream);

        protected string GetKey(T record)
        {
            var idField = record.GetType().GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(IdAttribute), true).Any());

            if (idField == null || idField.GetValue(record) == null)
            {
                // Handle null case, e.g., throw an exception or return a default value
                throw new ArgumentException($"Record does not have a valid field with 'IdFieldAttribute': {record}");
            }
            return idField.GetValue(record).ToString();
        }
    }
}