using GyroPlayer.Core.Interfaces;

namespace GyroPlayer.Core
{
    public class Artist : IArtist
    {
        public Artist(string artistName)
        {
            Name = artistName;
        }

        public string Name { get; private set; }

        public bool Equals(IArtist other)
        {
            return other != null &&
                !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(other.Name) && Name.Equals(other.Name);
        }
    }
}