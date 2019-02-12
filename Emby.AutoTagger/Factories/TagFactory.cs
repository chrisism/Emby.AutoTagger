using System.Collections.Generic;
using System.IO;
using Emby.AutoTagger.Domain;
using MediaBrowser.Model.Serialization;

namespace Emby.AutoTagger.Factories
{
    public class TagFactory
    {
        private readonly IJsonSerializer jsonSerializer;

        public TagFactory(IJsonSerializer jsonSerializer)
        {
            this.jsonSerializer = jsonSerializer;
        }

        public IEnumerable<Tag> CreateTagRuleset()
        {
            Stream resourceStrm = this.GetType().Assembly.GetManifestResourceStream(typeof(Plugin).Namespace + ".tagrules.json");
            TagsContainer container = this.jsonSerializer.DeserializeFromStream<TagsContainer>(resourceStrm);

            return container.Tags;
        }
    }
}
