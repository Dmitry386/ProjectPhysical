namespace Packages.DVUnityUtilities.Runtime.Other.TextMeshFormatting
{
    public class StringFormatContainer
    {
        public string Format;

        public StringFormatContainer(string format)
        {
            Format = format;
        }

        public string GetFormatted(params object[] parameters)
        {
            return string.Format(Format, parameters);
        }
    }
} 