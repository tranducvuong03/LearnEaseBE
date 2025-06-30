using Google.Cloud.TextToSpeech.V1;
using static Google.Cloud.Speech.V1.RecognitionConfig.Types;

namespace LearnEase.Service
{

    public class AudioGenerator
    {
        public async Task<byte[]> GenerateMp3Async(string text)
        {
            var client = await TextToSpeechClient.CreateAsync();
            var input = new SynthesisInput { Text = text };

            var voice = new VoiceSelectionParams
            {
                LanguageCode = "en-GB",
                SsmlGender = SsmlVoiceGender.Female
            };

            var config = new AudioConfig
            {
                AudioEncoding = Google.Cloud.TextToSpeech.V1.AudioEncoding.Mp3
            };


            var response = await client.SynthesizeSpeechAsync(input, voice, config);
            return response.AudioContent.ToByteArray();
        }
    }
}
