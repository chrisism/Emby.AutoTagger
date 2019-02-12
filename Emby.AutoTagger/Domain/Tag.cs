using System.Collections.Generic;

namespace Emby.AutoTagger.Domain
{
    public class Tag
    {
        public string Name { get; set; }

        public IEnumerable<TagRule> Rules { get; set; }
    }
}