    namespace  Medica.Uk.TechnicalDemonstration.DataModel
    {
        public class JsonDataModel : DataModel
        {
            [Id]
            public string? PatientId { get; set; }
            public string? AppointmentID { get; set; }
            public string? Gender { get; set; }
            public string? ScheduledDay { get; set; }
            public string? AppointmentDay { get; set; }
            public string? Age { get; set; }
            public string? Neighbourhood { get; set; }
            public string? Scholarship { get; set; }
            public string? Hipertension { get; set; }
            public string? Diabetes { get; set; }
            public string? Alcoholism { get; set; }
            public string? Handcap { get; set; }
            public string? SMS_received { get; set; }
            public string? No_show { get; set; }
        }
    }