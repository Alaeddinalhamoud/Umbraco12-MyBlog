using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Dashboards;

namespace MyBlog.WebUI.App_Plugins.FeedbackDashboard
{
    public class FeedbackDashboard : IDashboard
    {
        public string[] Sections => new[] { Constants.Applications.Content };

        public IAccessRule[] AccessRules
        {
            get
            {
                return new IAccessRule[]
                {
                    new AccessRule { Type = AccessRuleType.Grant, Value = Constants.Security.AdminGroupAlias }
                };
            }
        }

        public string? Alias => "Feed_backs";

        //  public string? View => "/App_Plugins/FeedbackDashboard/feedback.html";
        public string? View => "backoffice/Feedbacks";

    }
}
