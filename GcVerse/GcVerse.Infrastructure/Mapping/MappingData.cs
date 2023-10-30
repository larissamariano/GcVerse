using Dapper;
using GcVerse.Models.Category;
using GcVerse.Models.Content;
using GcVerse.Models.Shared;
using GcVerse.Models.User;
using System.ComponentModel;
using System.Reflection;

namespace GcVerse.Infrastructure.Mapping
{
    public class MappingData
    {
        public static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return (attrib?.Description ?? member.Name).ToLower();
        }

        public static void Mapping()
        {
            //var map = new CustomPropertyTypeMap(typeof(BaseCategory), (type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(BaseCategory), map);

            //map = new CustomPropertyTypeMap(typeof(SubCategory), (type, columnName)
            //=> type.GetProperties().First(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(SubCategory), map);

            //map = new CustomPropertyTypeMap(typeof(BaseImage), (type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(BaseImage), map);

            //map = new CustomPropertyTypeMap(typeof(BaseContent),
            //(type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(BaseContent), map);

            //map = new CustomPropertyTypeMap(typeof(ListTopic),
            //(type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(ListTopic), map);

            //map = new CustomPropertyTypeMap(typeof(NewsContent),
            //(type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(NewsContent), map);

            //map = new CustomPropertyTypeMap(typeof(QuizzContent),
            //(type, columnName) =>
            //type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(QuizzContent), map);

            //map = new CustomPropertyTypeMap(typeof(QuizzResult),
            //(type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(QuizzResult), map);

            //map = new CustomPropertyTypeMap(typeof(QuizzQuestion),
            //(type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(QuizzQuestion), map);

            //map = new CustomPropertyTypeMap(typeof(QuestionOption),
            //(type, columnName)
            //=> type.GetProperties().First(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            //Dapper.SqlMapper.SetTypeMap(typeof(QuestionOption), map);

        }
    }
}
