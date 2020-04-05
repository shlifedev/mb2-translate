using CsvHelper.Configuration.Attributes;

public class TranslateData
{
    [Name("Id")]
    public string Id { get; set; }
    [Name("Original")]
    public string Original { get; set; }
    [Name("Translate")]
    public string Translate { get; set; }
    [Name("Filename")]
    public string Filename { get; set; }
    [Name("Module")]
    public string Module { get; set; }
}