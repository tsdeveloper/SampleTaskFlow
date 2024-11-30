namespace API.Controllers.Test.Builder.Entities
{
    public class AppConfigBuilder : BaseBuilder<Core.Entities.AppConfig>
    {
        public AppConfigBuilder Default()
        {
            _instance.MaxLimitTask = 10;
            return this;

        }
    }
}
