using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerBook.Models.Responses
{
    public class PagedResponse<T>
    {
        private readonly T[] _data;
        public IEnumerable<T> Data => _data;
        public int From { get; }
        public int To { get; }

        public PagedResponse(int from, int to, IEnumerable<T> data)
        {
            _data = data.ToArray();
            From = from;
            To = to;
        }
    }
}
