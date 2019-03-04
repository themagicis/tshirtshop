using Microsoft.Extensions.Options;

namespace App.Common.Config
{
    public class ConfigOptions<T> where T : class, new()
    {
        public ConfigOptions(IOptions<T> options)
        {
            if (options != null)
            {
                Options = options.Value;
            }
        }

        public T Options { get; private set; }

        public void SetOptions(T options)
        {
            Options = options;
        }
    }
}