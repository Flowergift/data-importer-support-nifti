    using System;
using FileHelpers;
namespace  Medica.Uk.TechnicalDemonstration.DataModel
    {
        [DelimitedRecord(",")]
        public class CsvDataModel : DataModel
        {
            [Id]
            public string? PatientId { get; set; }
            public string? Name { get; set; }
            [FieldQuoted]
            public string? DateOfBirth { get; set; }
            public string? Gender { get; set; }
            [FieldQuoted]
            public string? MedicalConditions { get; set; }
            [FieldQuoted]
            public string? Medications { get; set; }
            [FieldQuoted]
            public string? Allergies { get; set; }
            [FieldQuoted]
            public string? LastAppointmentDate { get; set; }
        }
    }