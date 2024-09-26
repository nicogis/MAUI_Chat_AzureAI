using Markdig;
using System.Globalization;

namespace ChatAI.Converters;



public class MarkdownToHtmlConverter : IValueConverter
{
    
    private static readonly MarkdownPipeline readonlypipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
    

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            
            return Markdown.ToHtml(stringValue, readonlypipeline);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
