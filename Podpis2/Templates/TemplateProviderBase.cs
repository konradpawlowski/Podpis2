using System.Resources;
using Podpis2.Interfaces;

namespace Podpis2.Templates
{
    public abstract class TemplateProviderBase : ITemplateProvider
    {
   //     private readonly ILocationManager locationManager;

        private readonly string templateName;

        private readonly Lazy<string> template;

        public string Template
        {
            get
            {
                return this.template.Value;
            }
        }

        protected TemplateProviderBase(string templateName)
        {
          
            if (string.IsNullOrEmpty(templateName))
            {
                throw new System.ArgumentException("Template name cannot be empty.", "templateFileName");
            }
        //    this.locationManager = locationManager;
            this.templateName = templateName;
            this.template = new Lazy<string>(new Func<string>(this.ReadTemplate));
        }

        private string ReadTemplate()
        {
            string result;
            ResourceManager rmStrings = new ResourceManager("Podpis2.res.Podpis", System.Reflection.Assembly.GetAssembly(typeof(Podpis2.res.Podpis)));
            string select = rmStrings.GetString(templateName);


            var array = select.Split(Environment.NewLine.ToCharArray());
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            foreach (string text in array)
            {
                string value = text.Trim().Replace(System.Environment.NewLine, string.Empty);
                stringBuilder.Append(value);
            }
            result = stringBuilder.ToString();
            return result;

          
        }
    }
}
