using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
    public partial class StudentPersonalData
    {
        [JsonProperty("GetStudentPersonalResult")]
        public GetStudentPersonalResult[] GetStudentPersonalResult { get; set; }
    }

    public partial class GetStudentPersonalResult
    {
        [JsonProperty("ArabicName")]
        public string ArabicName { get; set; }

        [JsonProperty("BirthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("EnglishName")]
        public string EnglishName { get; set; }

        [JsonProperty("GenderArabic")]
        public string GenderArabic { get; set; }

        [JsonProperty("GenderEnglish")]
        public string GenderEnglish { get; set; }

        [JsonProperty("GenderID")]
        public string GenderId { get; set; }

        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("NationalityArabic")]
        public string NationalityArabic { get; set; }

        [JsonProperty("NationalityEnglish")]
        public string NationalityEnglish { get; set; }

        [JsonProperty("NationalityID")]
        public string NationalityId { get; set; }

        [JsonProperty("StudentContacts")]
        public StudentContact[] StudentContacts { get; set; }
    }

    public partial class StudentContact
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("EmployerCode")]
        public string EmployerCode { get; set; }

        [JsonProperty("EmployerNameArabic")]
        public string EmployerNameArabic { get; set; }

        [JsonProperty("EmployerNameEnglish")]
        public string EmployerNameEnglish { get; set; }

        [JsonProperty("EmployerTypeArabic")]
        public string EmployerTypeArabic { get; set; }

        [JsonProperty("EmployerTypeEnglish")]
        public string EmployerTypeEnglish { get; set; }

        [JsonProperty("EmployerTypeID")]
        public string EmployerTypeId { get; set; }

        [JsonProperty("Guardian")]
        public bool Guardian { get; set; }

        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("Relationship")]
        public string Relationship { get; set; }

        [JsonProperty("RelationshipID")]
        public string RelationshipId { get; set; }

        [JsonProperty("MaritalstatusID")]
        public string MaritalstatusId { get; set; }
    }

    public partial class StudentPersonalData
    {
        public static StudentPersonalData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<StudentPersonalData>(json, Converter.Settings);
        }
    }

    
}
