using System;
using InquirerForAndroid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Services
{
    public class JsonConverters
    {
        public static JsonConverter[] AllConverters = new JsonConverter[]
        {
            new SurveyInfoConverter(),
            new BlockInfoConverter(),
            new QuestionInfoConverter(),
            new EnterpriseInfoConverter(),
        };

        private class SurveyInfoConverter : CustomCreationConverter<ISurveyInfo>
        {
            public override ISurveyInfo Create(Type objectType)
            {
                return new SurveyInfo();
            }
        }

        private class BlockInfoConverter : CustomCreationConverter<IBlockInfo>
        {
            public override IBlockInfo Create(Type objectType)
            {
                return new BlockInfo();
            }
        }
        private class QuestionInfoConverter : CustomCreationConverter<IQuestionInfo>
        {
            public override IQuestionInfo Create(Type objectType)
            {
                return new QuestionInfo();
            }
        }
        private class EnterpriseInfoConverter : CustomCreationConverter<IEnterpriseInfo>
        {
            public override IEnterpriseInfo Create(Type objectType)
            {
                return new EnterpriseInfo();
            }
        }
    }

}