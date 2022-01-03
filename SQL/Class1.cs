using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.Models;

namespace SQL
{
    public class SQL
    {
        const string firstSQL = "a=a";
        List<string> fsList = new List<string>();
        string filterStr = ""; 

        public List<Cat>  Filter(List<Cat> recList, string filterSQL)
		{
            //Build sql
            fsList.Add(filterSQL);
            fsList.Add("gender=male");


            List<Cat> cm = new List<Cat>();
            //cm = (List<Cat>)recList.Select<List<Cat>,List<Cat>>(filterStr);

            
           


            return cm;
		}
		
    }
}
