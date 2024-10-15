using System.Globalization;
using System.Text;
using CsvHelper;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using OutputFormatterSample.Models;

namespace OutputFormatterSample;

public class CsvOutputFormatter : TextOutputFormatter
{
    public string ContentType { get; } = "text/csv";

    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ContentType));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    // optional, but makes sense to restrict to a specific condition
    protected override bool CanWriteType(Type? type)
    {
        if (typeof(Person).IsAssignableFrom(type) || typeof(IEnumerable<Person>).IsAssignableFrom(type))
        {
            return base.CanWriteType(type);
        }
        return false;
    }

    // this needs to be overwritten
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;

        var csv = new CsvWriter(new StreamWriter(response.Body), CultureInfo.InvariantCulture);

        IEnumerable<Person>? persons;
        if (context.Object is IEnumerable<Person>)
        {
            persons = context.Object as IEnumerable<Person>;
        }
        else
        {
            var person = context.Object as Person;
            persons = new List<Person> { person };
        }
        await csv.WriteRecordsAsync(persons);
    }
}
