﻿using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Trendsdotnet.Models.ComparisonItems
{
    class Multiline : IComparisonItem
    {
        public JObject geo { get; set; } = new JObject();
        public ComplexKeywordsRestriction complexKeywordsRestriction { get; set; }
        public Multiline(string term, string geo)
        {
            //this.geo = new RestrictionGeo() { country = geo };
            this.complexKeywordsRestriction = new ComplexKeywordsRestriction(term);
        }
    }
    class ComplexKeywordsRestriction 
    {
        public ComplexKeyword[] keyword { get; set; }
        public ComplexKeywordsRestriction(string term)
        {
            this.keyword = new ComplexKeyword[] { new ComplexKeyword() { value = term } };
        }
    }
    class ComplexKeyword
    {
        public string type { get; set; } = "BROAD";
        [JsonIgnore]
        private string _value { get; set; }
        public string value { get => _value; set { _value = value.Replace(" ", "+"); } }
    }
}
