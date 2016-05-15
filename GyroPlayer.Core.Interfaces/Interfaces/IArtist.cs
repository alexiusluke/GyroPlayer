using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace GyroPlayer.Core.Interfaces
{
    public interface IArtist : IEquatable<IArtist>
    {
        string Name { get; }                
    }
}
