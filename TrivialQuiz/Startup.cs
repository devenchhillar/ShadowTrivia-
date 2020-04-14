using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrivialQuiz.Startup))]
namespace TrivialQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
