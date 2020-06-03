using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkPadsForSaleDiscordNotifier
{
    class MarketEntry
    {
        public string Author { get; }
        public string Have { get; }
        public string Want { get; }
        public string Location { get; }
        public DateTime Created { get; }
        public string Id { get; }
        public string Permalink { get; }

        public MarketEntry(string author, string have, string want, string location, DateTime created, string id, string permalink)
        {
            Author = author;
            Have = have;
            Want = want;
            Location = location;
            Created = created;
            Id = id;
            Permalink = permalink;
        }

        protected bool Equals(MarketEntry other)
        {
            return string.Equals(Id, other.Id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((MarketEntry) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

        public static bool operator ==(MarketEntry left, MarketEntry right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MarketEntry left, MarketEntry right)
        {
            return !Equals(left, right);
        }
    }
}
