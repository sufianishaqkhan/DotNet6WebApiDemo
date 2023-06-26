using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOS
{
    public class GenericModel
    {
        public long? Id { get; set; }
        public long? ParentID { get; set; }
        public string? ItemCode { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? Guid { get; set; }
        public string? Type { get; set; }
        public string? MyDesignation { get; set; }
        public long Members { get; set; }

        public DateTime? DateAdded { get; set; }
        public List<GenericModel> childModels { get; set; }

        public GenericModel()
        {
            childModels = new List<GenericModel>();
        }
    }
}
