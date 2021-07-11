using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text.Json;
using HtmlAgilityPack;

namespace Hilti_parser
{
    public class priceGrubber
    {
        List<Array> links = new List<Array>();
        List<string> result = new List<string>();
        string resultTemplate = "Артикул;Артикул поставщика;Модель;Наименование;Ед. изм.;Цена;Валюта;Статус;Фасовка поставщика;Мин. партия;Ед. в упаковке;Вес, кг;Объем, м3;Единица размера (м, см, мм);Длина;Ширина;Высота;Мощность, Вт.;Напряжение, В;Страна изготовитель;Завод;Группа 1;Группа 2;Группа 3;Группа 4;Группа 5;Группа 6;EAN;Скидка от розничной до закупочной цены, % (только для загрузки от розничных цен);Скидка от розничной до учетной цены, % (только для загрузки от розничных цен)";

        public void grubPrices()
        {
            foreach (Array link in links)
            {
                string linkStr = (string)link.GetValue(1);
                if (linkStr.Contains('?'))
                {
                    string html = @$"https://hilti.ru{link.GetValue(1)}";
                    Console.WriteLine($"Link is {html}");
                    HtmlWeb web = new HtmlWeb();
                    var document = web.Load(html);
                    //not working
                    //string price = document.DocumentNode.SelectSingleNode("//div[@class=\"a-price\"]/span").InnerText;

                    //div with <script> has class="js-tab-shop js-tab-content" and id="shop"
                    //string jsTabData = document.DocumentNode.SelectSingleNode("//div[@class=\"js-tab-shop\"").InnerHtml;
                    string scriptData = document.DocumentNode.SelectSingleNode("//div[@id=\"shop\"]/script").InnerText;
                    //cropping in 2 steps because I'm too lazy at sunday 6.26 AM to make it right in 1 line
                    string scriptDataPreCropped = scriptData.Substring(scriptData.IndexOf("productPageData") + 17);
                    string scriptDataCropped = scriptDataPreCropped.Remove(scriptDataPreCropped.IndexOf(';'));
                    dynamic jsJSONObject = JsonSerializer.Deserialize<Object>(scriptDataCropped);
                    var price = jsJSONObject.key_technical_attributes.price_data.standard.value;
                    

                    Console.WriteLine($"Price is {price}");
                    string priceResult = Regex.Replace(price, @"\s[A-Z]*", "");
                    string article = link.GetValue(0) + "H";
                    result.Add($"{article};{link.GetValue(0)};;;;{priceResult};RUB;;;;;;;;;;;;;;;;;;;;;;");
                }
                else
                {
                    continue;
                }
            }
            //string todayDate = DateTime.UtcNow.Date.ToString();
            using(StreamWriter sw = new StreamWriter(@$"../../../../Hilti prices.csv"))
            {
                sw.WriteLine(resultTemplate);
                foreach(string line in result)
                {
                    sw.WriteLine(line);
                }
            }
        }

        public priceGrubber()
        {
        }
        public priceGrubber(List<Array> list)
        {
            links = list;
        }
        
    }
}
