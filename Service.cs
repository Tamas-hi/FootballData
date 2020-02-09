using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData
{
    class Service
    {
        private readonly Uri serverUrl = new Uri("https://od-api.oxforddictionaries.com");

        /// <summary>
        /// A method which calls GET asynchronously and deserializes result from JSON. Handles exceptions e.g., no word found.
        /// </summary>
        /// <typeparam name="T">Type of the returned result.</typeparam>
        /// <param name="uri">The uri to fetch from. </param>
        /// <returns>The needed data or null</returns>
        private async Task<T> GetAsync<T>(Uri uri) where T : class
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("app_id", "737bf108"); // needed by API
                client.DefaultRequestHeaders.Add("app_key", "5f69062e1685769a904ffaa7c56b34cb"); // needed by API
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get the languages asynchronously using <see cref="GetAsync{T}(Uri)"/>
        /// </summary>
        /// <returns>The languages.</returns>
        public async Task<Languages> GetLanguageAsync()
        {
            return await GetAsync<Languages>(new Uri(serverUrl, "/api/v1/languages"));
        }

        /// <summary>
        /// Get the translations asynchronously using <see cref="GetAsync{T}(Uri)"/>
        /// </summary>
        /// <param name="source_translation_language">Source translation language.</param>
        /// <param name="word_id">Word ID.</param>
        /// <param name="target_translation_language">Target translation language.</param>
        /// <returns>Translations for the appropriate word.</returns>
        public async Task<Translations> GetTranslationAsync(string source_translation_language, string word_id, string target_translation_language)
        {
            return await GetAsync<Translations>(new Uri(serverUrl, "/api/v1/entries/" + source_translation_language + "/" + word_id + "/translations=" + target_translation_language));
        }

        /// <summary>
        /// Get the synonyms asynchronously using <see cref="GetAsync{T}(Uri)"/>
        /// </summary>
        /// <param name="source_lang">Source language.</param>
        /// <param name="word_id">Word ID.</param>
        /// <returns>Synonyms for the appropriate word. </returns>
        public async Task<SynonymsAndAntonyms> GetSynonymAsync(string source_lang, string word_id)
        {
            return await GetAsync<SynonymsAndAntonyms>(new Uri(serverUrl, "/api/v1/entries/" + source_lang + "/" + word_id + "/synonyms"));
        }

        /// <summary>
        /// Get the antonyms asynchronously using <see cref="GetAsync{T}(Uri)"/>
        /// </summary>
        /// <param name="source_lang">Source language.</param>
        /// <param name="word_id">Word ID.</param>
        /// <returns>Antonyms for the appropriate word.</returns>
        public async Task<SynonymsAndAntonyms> GetAntonymAsync(string source_lang, string word_id)
        {
            return await GetAsync<SynonymsAndAntonyms>(new Uri(serverUrl, "/api/v1/entries/" + source_lang + "/" + word_id + "/antonyms"));
        }

        /// <summary>
        /// Get the example sentences asynchronously using <see cref="GetAsync{T}(Uri)"/>
        /// </summary>
        /// <param name="source_lang">Source language.</param>
        /// <param name="word_id">Word ID.</param>
        /// <returns>Example sentences for the appropriate word.</returns>
        public async Task<Sentences> GetSentenceAsync(string source_lang, string word_id)
        {
            return await GetAsync<Sentences>(new Uri(serverUrl, "/api/v1/entries/" + source_lang + "/" + word_id + "/sentences"));
        }

    }
}
}
