using System;
using System.Collections.Generic;

namespace MyApi.Configuration.Authentication
{
    public class ApiKey
    {
        public ApiKey(int id, string owner, string key, DateTime created, IReadOnlyCollection<string> roles)
        {
            Id = id;
            Owner = owner;
            Key = key;
            Created = created;
            Roles = roles;
        }

        public int Id { get; }
        public string Owner { get; }
        public string Key { get; }
        public DateTime Created { get; }
        public IReadOnlyCollection<string> Roles { get; }
    }
}
