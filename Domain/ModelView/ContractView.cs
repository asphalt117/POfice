using System.Collections.Generic;
using Domain.Entities;

namespace Domain.ModelView
{
    public class ContractView
    {
        public List<Contract> Contracts;
        public string SelectedID { get; set; }
    }
}
