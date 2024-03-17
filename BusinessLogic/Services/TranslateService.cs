using Newtonsoft.Json;
using System.Net;

namespace KFitServer
{
    public static class TranslateService
    {
        private const string googleTranslaterAPIKey = "AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw&logld=vTE_20231206";
        private const string googleTranslateAPIURL = "https://translation.googleapis.com/language/translate/v2?";
        public static async Task<string> TranslateText(string input, string langCode, string targetLangCode)

        {
            string url = $"{googleTranslateAPIURL}key={googleTranslaterAPIKey}&logld=vTE_20231206&q={input}&source={langCode}&target={targetLangCode}";

            TranslateResponse translation = null;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string json = await webClient.DownloadStringTaskAsync(url);
                    translation = JsonConvert.DeserializeObject<TranslateResponse>(json);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return translation.data.Translations.FirstOrDefault().translatedText;

        }
    }
}
