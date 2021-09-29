using JetBrains.Annotations;
using NEXTjeugd.Personen;

namespace NEXTjeugd.Clienten
{
    public class Client :Persoon
    {
        [CanBeNull]
        public virtual string Naam { get; set; }

        public Client()
        {

        }

        public Client(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }
    }
}