using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BookService.Responses
{
    public class PaginationInfo<T>
    {
        public int Count { get; set; }
        public int PagesCount { get; set; }
        //public int PageSize  { get; set; }
        public int CurrentPage { get; set; }
        public List<T> PaginationData { get; set; }
    }
}
