namespace EasySms
{
    /// <summary>
    /// SMS Sending Request
    /// </summary>
    public class SmsTemplate
    {
        /// <summary>
        /// Initialize the SMS message content
        /// </summary>
        /// <param name="templateName">The name of the template set in the configuration file</param>
        /// <param name="parameter">Parameter that will be sent</param>
        public SmsTemplate(string templateName, object parameter)
        {
            TemplateName = templateName;
            Parameter    = parameter;
        }

        /// <summary>
        /// Template, you need to configure the code corresponding to the template name in the configuration file
        /// </summary>
        public string TemplateName { get; }

        /// <summary>
        /// Obtaining Template Parameters
        /// </summary>
        public object Parameter { get; }
    }
}
