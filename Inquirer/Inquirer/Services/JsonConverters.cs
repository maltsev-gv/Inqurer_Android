using System;
using InquirerForAndroid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Services
{
    public class JsonConverters
    {
        public static JsonConverter[] AllConverters = 
        {
            new CustomJsonConverter<ISurveyInfo, SurveyInfo>(),
            new CustomJsonConverter<IBlockInfo, BlockInfo>(),
            new CustomJsonConverter<IQuestionInfo, QuestionInfo>(),
            new CustomJsonConverter<IEnterpriseInfo, EnterpriseInfo>(),
            new CustomJsonConverter<IDiagramInfo, DiagramInfo>(),
            new CustomJsonConverter<IWishInfo, WishInfo>(),
            new CustomJsonConverter<IWishOptionInfo, WishOptionInfo>(),
        };

        public class CustomJsonConverter<I, T> : CustomCreationConverter<I> where T: class, I, new()
        {
            public override I Create(Type objectType)
            {
                return new T();
            }
        }
    }

}