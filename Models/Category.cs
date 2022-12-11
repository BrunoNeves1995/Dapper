using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Models
{
    public class Category
    {
        public Category(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; private set; }
        public string? Title { get; private set; }
    }
}