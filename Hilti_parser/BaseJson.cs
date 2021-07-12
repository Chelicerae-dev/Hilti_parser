using System;
using System.Text.Json;

namespace Hilti_parser
{
    public class BaseJson
    {
        public bool fleet_contract;
        public bool single_item_data;
        public RangePage range_page;
        //public string fleet_price_type;

        public BaseJson()
        {
            //range_page = JsonSerializer.Deserialize<RangePage>(range_page);
        }
    }
}
