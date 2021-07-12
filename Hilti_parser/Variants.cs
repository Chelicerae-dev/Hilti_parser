using System;
using System.Text.Json;
namespace Hilti_parser
{
    public class Variants
    {
        public string id;
        public string name;
        public JsonElement filterAttributes;
        public JsonElement[] galleryImages;
        public string scenario;
        public bool purchasable;
        public string hol_relative_url;
        public string quantity_box_sales_uom;
        public string total_counter_label;
        public string product_type;
        public int minimum_order_quantity;
        public int pack_quantity;
        public int round_up_quantity;
        public JsonElement[] key_technical_attributes;
        public bool new_flag;
        public bool base_product;
        public PriceData price_data;
        public string billing_cycle;
        public string unit_code;
        public string base_unit;
        public string unit_label;
        public int normalized_minimum_order_quantity;
        public int normalized_round_up_quantity;
        public bool hidden;
        public bool trial_available;
        public string trial_url_link;


        public Variants()
        {
        }
    }
}
