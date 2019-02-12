using System;
using System.IO;
using System.Threading.Tasks;
using MediaBrowser.Model.Serialization;
using Newtonsoft.Json;

namespace Emby.AutoTagger.Tests
{
    public class Serializer : IJsonSerializer
    {
        public void SerializeToStream(object obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public void SerializeToFile(object obj, string file)
        {
            throw new NotImplementedException();
        }

        public object DeserializeFromFile(Type type, string file)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFromFile<T>(string file) where T : class
        {
            throw new NotImplementedException();
        }

        public T DeserializeFromStream<T>(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }

        public Task<T> DeserializeFromStreamAsync<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFromString<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public object DeserializeFromStream(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public Task<object> DeserializeFromStreamAsync(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public object DeserializeFromString(string json, Type type)
        {
            throw new NotImplementedException();
        }

        public string SerializeToString(object obj)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFromSpan<T>(ReadOnlySpan<char> text)
        {
            throw new NotImplementedException();
        }

        public object DeserializeFromSpan(ReadOnlySpan<char> json, Type type)
        {
            throw new NotImplementedException();
        }

        public object DeserializeFromBytes(ReadOnlySpan<byte> bytes, Type type)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFromBytes<T>(ReadOnlySpan<byte> bytes)
        {
            throw new NotImplementedException();
        }
    }
}