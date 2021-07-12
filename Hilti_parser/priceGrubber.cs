using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using HtmlAgilityPack;
//using Newtonsoft.Json;

namespace Hilti_parser
{
    public class priceGrubber
    {
        List<Array> links = new List<Array>();
        List<string> result = new List<string>();
        string resultTemplate = "Артикул;Артикул поставщика;Модель;Наименование;Ед. изм.;Цена;Валюта;Статус;Фасовка поставщика;Мин. партия;Ед. в упаковке;Вес, кг;Объем, м3;Единица размера (м, см, мм);Длина;Ширина;Высота;Мощность, Вт.;Напряжение, В;Страна изготовитель;Завод;Группа 1;Группа 2;Группа 3;Группа 4;Группа 5;Группа 6;EAN;Скидка от розничной до закупочной цены, % (только для загрузки от розничных цен);Скидка от розничной до учетной цены, % (только для загрузки от розничных цен)";

        public void grubPrices()
        {
            int iterator = 0;
            foreach (Array link in links)
            {
                iterator++;
                string linkStr = (string)link.GetValue(1);

                if (linkStr.Contains('?') && linkStr.Contains("/c/"))
                {
                    string html = @$"https://hilti.ru{link.GetValue(1)}";

                    Console.WriteLine($"{iterator}. Article {link.GetValue(0)}.");
                    Console.WriteLine($"Link is {html}");
                    HtmlWeb web = new HtmlWeb();
                    var document = web.Load(html);
                    double price = 0;
                    //not working
                    //string price = document.DocumentNode.SelectSingleNode("//div[@class=\"a-price\"]/span").InnerText;

                    //div with <script> has class="js-tab-shop js-tab-content" and id="shop"
                    //string jsTabData = document.DocumentNode.SelectSingleNode("//div[@class=\"js-tab-shop\"").InnerHtml;

                    string scriptData = document.DocumentNode.SelectSingleNode("//div[@id=\"shop\"]/script").InnerText;

                    //cropping in 2 steps because I'm too lazy at sunday 6.26 AM to make it right in 1 line
                    string scriptDataPreCropped = scriptData.Substring(scriptData.IndexOf("productPageData") + 17);
                    string scriptDataCropped = scriptDataPreCropped.Remove(scriptDataPreCropped.IndexOf(';'));
                    //string scriptDataCropped = scriptDataFullyCropped.Replace("\"", "\"\"");
                   
                    //deserealizing JSON
                    //BaseJson jsJSONObject = JsonSerializer.Deserialize<BaseJson>(scriptDataCropped);

                    //preparing data for jsonReader
                    /*byte[] scriptDataBytes = System.Text.Encoding.ASCII.GetBytes(scriptDataCropped);
                    long variantsStart = 0;
                    long variantsFinish = 0;

                    Utf8JsonReader jsonReader = new Utf8JsonReader(scriptDataBytes);*/

                    /*while(jsonReader.Read())
                    {
                        //Console.WriteLine(jsonReader.TokenType);
                        if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.GetString() == "variants")
                        {
                            variantsStart = jsonReader.BytesConsumed;
                            Console.WriteLine(jsonReader.GetString());
                            Console.ReadKey();
                        }
                        if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.GetString() == "technical_attributes")
                        {
                            variantsFinish = jsonReader.BytesConsumed;
                        }

                    }
                    byte[] variantsBytes = scriptDataBytes[(int)variantsStart..(int)variantsFinish];
                    string variantsTemp = System.Text.Encoding.ASCII.GetString(variantsBytes);
                    string variantsData = variantsTemp.Substring(1, variantsTemp.Length - 27);
                    //Array variantsArr = variantsData.Split("},{");
                    Variants[] variants = JsonSerializer.Deserialize<Variants[]>(variantsData);*/


                    //JsonDocument jsonParsed = JsonDocument.ParseValue(ref jsonReader);
                    try
                    {
                        using (JsonDocument jsonParsed = JsonDocument.Parse(scriptDataCropped))
                        {
                            JsonElement root = jsonParsed.RootElement;
                            JsonElement rangePage = root.GetProperty("range_page");  //[2]
                            JsonElement variants = rangePage.GetProperty("variants");  //[5]
                            foreach (JsonElement variant in variants.EnumerateArray())
                            {
                                if (variant.GetProperty("id").GetString() == (string)link.GetValue(0))
                                {
                                    price = variant.GetProperty("price_data").GetProperty("standard").GetProperty("value").GetDouble();
                                }
                                else continue;
                            }
                        }
                    }
                    catch (JsonException)
                    {
                        Console.WriteLine($"JSON Exception raised!");
                        continue;
                    }
                    if (price == 0)
                    {
                        Console.WriteLine(scriptDataCropped);
                        Console.ReadLine();
                    }
                    Console.WriteLine($"Price is {price}");
                    string article = link.GetValue(0) + "H";
                    result.Add($"{article};{link.GetValue(0)};;;;{price};RUB;;;;;;;;;;;;;;;;;;;;;;");

                    //JsonElement variants = jsonParsed.

                    //JsonDocument jsJSONObject = JsonDocument.Parse(scriptDataCropped);



                    //getting variants array step by step
                    //RangePage rangePage = JsonSerializer.Deserialize<RangePage>(jsJSONObject.range_page);
                    //Variants[] jsonVariants = JsonSerializer.Deserialize<Variants[]>(rangePage.variants);
                    //IList<Variants> jsonVariants = jsJSONObject.range_page.variants;
                    //creating price variable for further use (string at the moment)
                    //decimal price = 0;

                    /*foreach(var variant in jsJSONObject.range_page.variants)
                    {
                        if (variant.id == link.GetValue(0))
                        {
                            price = variant.price_data.standard.value;
                        }
                        else continue;
                    }
                    
                    

                    Console.WriteLine($"Price is {price}");
                    string article = link.GetValue(0) + "H";
                    result.Add($"{article};{link.GetValue(0)};;;;{price};RUB;;;;;;;;;;;;;;;;;;;;;;");*/
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
