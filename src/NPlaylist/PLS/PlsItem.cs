namespace NPlaylist.PLS.PlsParts
{
    public class PlsItem : BasePlaylistItem
    {
        public PlsItem(string path) : base(path)
        {

        }

        public override string Path
        {
            get => Tags.TryGetValue(PlsTagNames.Path, out var value) ? value : null;
            set => Tags[PlsTagNames.Path] = value;
        }

        public string Title
        {
            get => Tags.TryGetValue(PlsTagNames.Title, out var value) ? value : null;
            set => Tags[PlsTagNames.Title] = value;
        }

        public string Length
        {
            get => Tags.TryGetValue(PlsTagNames.Length, out var value) ? value : null;
            set => Tags[PlsTagNames.Length] = value;
        }
    }
}
