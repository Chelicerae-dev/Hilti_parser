using System;
using System.Text.Json;
using System.Collections.Generic;

namespace Hilti_parser
{
    public class RangePage
    {
        public string id;
        public string name;
        public string description;
        public JsonElement filters;
        public string media;
        public Variants[] variants;
        public string technical_attributes;
        public string key_technical_attributes;
        public bool new_flag;
        public string fleetable_flag;
        public bool hidden;

        public RangePage()
        {
        }
    }
}
